
define(function (require, exports, module) {
    var Upload = require("upload"), ProductIco, ImgsIco,
        Global = require("global"),
        Verify = require("verify"), VerifyObject, DetailsVerify, editor,
        doT = require("dot"),
        Easydialog = require("easydialog");
    require("pager");
    require("switch");
    var Params = {
        keyWords: "",
        pageIndex: 1,
        totalCount: 0
    };
    var Product = {};
    //添加页初始化
    Product.init = function (Editor) {
        editor = Editor;
        Product.bindEvent();
    }
    //绑定事件
    Product.bindEvent = function () {
        var _self = this;
        ProductIco = Upload.createUpload({
            element: "#productIco",
            buttonText: "选择产品图片",
            className: "edit-Product",
            data: { folder: '/Content/tempfile/', action: 'add', oldPath: "" },
            success: function (data, status) {
                if (data.Items.length > 0) {
                    _self.ProductImage = data.Items[0];
                    $("#productImg").attr("src", data.Items[0]);
                }
            }
        });
        $("#btnSaveProduct").on("click", function () {
            if (!VerifyObject.isPass()) {
                return;
            }
            Product.savaProduct();
        });

        VerifyObject = Verify.createVerify({
            element: ".verify",
            emptyAttr: "data-empty",
            verifyType: "data-type",
            regText: "data-text"
        });

        //编码是否重复
        $("#productCode").blur(function () {
            var _this = $(this);
            if (_this.val().trim() != "") {
                Global.post("/Products/IsExistsProductCode", {
                    code: _this.val()
                }, function (data) {
                    if (data.Status) {
                        _this.val("");
                        alert("产品编码已存在,请重新输入!");
                        _this.focus();
                    }
                });
            }
        })

        $("#productName").focus();
    }
    //保存产品
    Product.savaProduct = function () {
        var _self = this, attrlist = "", valuelist = "", attrvaluelist = "";

        $(".productattr").each(function () {
            var _this = $(this);
            attrlist += _this.data("id") + ",";
            valuelist += _this.find("select").val() + ",";
            attrvaluelist += _this.data("id") + ":" + _this.find("select").val() + ",";
        });

        var Product = {
            ProductID: _self.ProductID,
            ProductCode: $("#productCode").val().trim(),
            ProductName: $("#productName").val().trim(),
            GeneralName: $("#generalName").val().trim(),
            IsCombineProduct: 0,
            BrandID: $("#brand").val(),
            BigUnitID: $("#bigUnit").val().trim(),
            SmallUnitID: $("#smallUnit").val().trim(),
            BigSmallMultiple: $("#bigSmallMultiple").val().trim(),
            CategoryID: $("#categoryID").val(),
            Status: $("#status").prop("checked") ? 1 : 0,
            AttrList: attrlist,
            ValueList: valuelist,
            AttrValueList: attrvaluelist,
            CommonPrice: $("#commonprice").val(),
            Price: $("#price").val(),
            Weight: $("#weight").val(),
            IsNew: $("#isNew").prop("checked") ? 1 : 0,
            IsRecommend: $("#isRecommend").prop("checked") ? 1 : 0,
            EffectiveDays: $("#effectiveDays").val(),
            DiscountValue:1,
            ProductImage: _self.ProductImage,
            ShapeCode: $("#shapeCode").val(),
            Description: encodeURI(editor.getContent())
        };

        Global.post("/Products/SavaProduct", {
            product: JSON.stringify(Product)
        }, function (data) {
            if (data.ID.length > 0) {
                location.href = "/Products/ProductDetail/" + data.ID;
            }
        });
    }
    //列表页初始化
    Product.initList = function () {
        Product.getList();
        Product.bindListEvent();
    }
    //绑定列表页事件
    Product.bindListEvent = function () {
        require.async("search", function () {
            $(".searth-module").searchKeys(function (keyWords) {
                Params.keyWords = keyWords;
                Product.getList();
            });
        });
    }
    //获取产品列表
    Product.getList = function () {
        var _self = this;
        $("#product-items").nextAll().remove();
        Global.post("/Products/GetProductList", Params, function (data) {
            doT.exec("template/products/product_list.html", function (templateFun) {
                var innerText = templateFun(data.Items);
                innerText = $(innerText);
                $("#product-items").after(innerText);

                //绑定启用插件
                innerText.find(".status").switch({
                    open_title: "点击上架",
                    close_title: "点击下架",
                    value_key: "value",
                    change: function (data,callback) {
                        _self.editStatus(data, data.data("id"), data.data("value"), callback);
                    }
                });
                innerText.find(".isnew").switch({
                    open_title: "设为新品",
                    close_title: "取消新品",
                    value_key: "value",
                    change: function (data, callback) {
                        _self.editIsNew(data, data.data("id"), data.data("value"), callback);
                    }
                });
                innerText.find(".isrecommend").switch({
                    open_title: "点击推荐",
                    close_title: "取消推荐",
                    value_key: "value",
                    change: function (data, callback) {
                        _self.editIsRecommend(data, data.data("id"), data.data("value"), callback);
                    }
                });
            });
            $("#pager").paginate({
                total_count: data.TotalCount,
                count: data.PageCount,
                start: Params.pageIndex,
                display: 5,
                border: true,
                border_color: '#fff',
                text_color: '#333',
                background_color: '#fff',
                border_hover_color: '#ccc',
                text_hover_color: '#000',
                background_hover_color: '#efefef',
                rotate: true,
                images: false,
                mouse: 'slide',
                onChange: function (page) {
                    Params.pageIndex = page;
                    Product.getList();
                }
            });
        });
    }
    //更改产品状态
    Product.editStatus = function (obj, id, status, callback) {
        var _self = this;
        Global.post("/Products/UpdateProductStatus", {
            productid: id,
            status: status ? 0 : 1
        }, function (data) {
            !!callback && callback(data.Status);
        });
    }
    //更改产品是否新品
    Product.editIsNew = function (obj, id, status, callback) {
        var _self = this;
        Global.post("/Products/UpdateProductIsNew", {
            productid: id,
            isnew: !status
        }, function (data) {
            !!callback && callback(data.Status);
        });
    }
    //更改产品是否推荐
    Product.editIsRecommend = function (obj, id, status, callback) {
        var _self = this;
        Global.post("/Products/UpdateProductIsRecommend", {
            productid: id,
            isRecommend: !status
        }, function (data) {
            !!callback && callback(data.Status);
        });
    }
    //初始化编辑页数据
    Product.initEdit = function (model, Editor) {
        var _self = this;
        editor = Editor;
        model = JSON.parse(model.replace(/&quot;/g, '"'));
        _self.bindDetailEvent(model);
        _self.bindDetail(model);
        _self.getChildList(model);
    }
    //获取详细信息
    Product.bindDetail = function (model) {
        var _self = this;
        _self.ProductID = model.ProductID;
        $("#productName").val(model.ProductName);
        $("#productCode").text(model.ProductCode);
        $("#generalName").val(model.GeneralName);
        $("#shapeCode").val(model.ShapeCode);

        //截取绑定属性值
        var list = model.AttrValueList.split(',');
        for (var i = 0, j = list.length; i < j; i++) {
            $("#" + list[i].split(':')[0]).val(list[i].split(':')[1]);
        }

        $("#brand").val(model.BrandID);
        $("#smallUnit").val(model.SmallUnitID);
        $("#bigUnit").val(model.BigUnitID);

        $("#bigSmallMultiple").val(model.BigSmallMultiple);
        $("#commonprice").val(model.CommonPrice);
        $("#price").val(model.Price);
        $("#weight").val(model.Weight);
        $("#effectiveDays").val(model.EffectiveDays);

        $("#status").prop("checked", model.Status == 1);
        $("#isNew").prop("checked", model.IsNew == 1);
        $("#isRecommend").prop("checked", model.IsRecommend == 1);

        $("#productImg").attr("src", model.ProductImage);
        
        _self.ProductImage = model.ProductImage;
        
        editor.ready(function () {
            editor.setContent(decodeURI(model.Description));
        });
    }
    //详情页事件
    Product.bindDetailEvent = function (model) {
        var _self = this, count = 1;

        //保存产品信息
        $("#btnSaveProduct").on("click", function () {
            if (!VerifyObject.isPass()) {
                return;
            }
            Product.savaProduct();
        });

        VerifyObject = Verify.createVerify({
            element: ".verify",
            emptyAttr: "data-empty",
            verifyType: "data-type",
            regText: "data-text"
        });
        //编辑图片
        ProductIco = Upload.createUpload({
            element: "#productIco",
            buttonText: "更换产品图片",
            className: "edit-Product",
            data: { folder: '/Content/tempfile/', action: 'add', oldPath: model.ProductImage },
            success: function (data, status) {
                if (data.Items.length > 0) {
                    _self.ProductImage = data.Items[0];
                    $("#productImg").attr("src", data.Items[0] + "?" + count++);
                }
            }
        });
        //切换内容
        $(".show-nav-ul li").click(function () {
            var _this = $(this);
            _this.addClass("hover");
            _this.siblings().removeClass("hover");
            $("#productinfo").hide();
            $("#childproduct").hide();
            $("#" + _this.data("id") + "").removeClass("hide").show();
        });

        $("#addDetails").on("click", function () {
            $(".show-nav-ul li").eq(0).removeClass("hover");
            $(".show-nav-ul li").eq(1).addClass("hover");
            $("#productinfo").hide();
            $("#childproduct").removeClass("hide").show();
            _self.showTemplate(model, "");
        });
    }
    //子产品列表
    Product.getChildList = function (model) {
        var _self = this;
        $("#header-items").nextAll().remove();
        doT.exec("template/products/productdetails_list.html", function (templateFun) {
            var innerText = templateFun(model.ProductDetails);
            innerText = $(innerText);
            $("#header-items").after(innerText);

            //绑定启用插件
            innerText.find(".status").switch({
                open_title: "点击启用",
                close_title: "点击禁用",
                value_key: "value",
                change: function (data, callback) {
                    _self.editDetailsStatus(data, data.data("id"), data.data("value"), callback);
                }
            });

            innerText.find(".ico-edit").click(function () {
                _self.showTemplate(model, $(this).data("id"));
            });
        });
    }
    //更改子产品状态
    Product.editDetailsStatus = function (obj, id, status, callback) {
        var _self = this;
        Global.post("/Products/UpdateProductDetailsStatus", {
            productdetailid: id,
            status: status ? 0 : 1
        }, function (data) {
            !!callback && callback(data.Status);
        });
    }
    //添加/编辑子产品
    Product.showTemplate = function (model, id) {
        var _self = this, count = 1;
        doT.exec("template/products/productdetails_add.html", function (templateFun) {

            var html = templateFun(model.Category.SaleAttrs);

            Easydialog.open({
                container: {
                    id: "productdetails-add-div",
                    header: !id ? "添加子产品" : "编辑子产品",
                    content: html,
                    yesFn: function () {

                        if (!DetailsVerify.isPass()) {
                            return false;
                        }

                        var attrlist = "", valuelist = "", attrvaluelist = "", desc = "";

                        $(".productattr").each(function () {
                            var _this = $(this);
                            attrlist += _this.data("id") + ",";
                            valuelist += _this.find("select").val() + ",";
                            attrvaluelist += _this.data("id") + ":" + _this.find("select").val() + ",";
                            //desc += "[" + _this.find(".column-name").html() + _this.find("select option:selected").text() + "]";
                        });

                        var Model = {
                            ProductDetailID: id,
                            ProductID: model.ProductID,
                            DetailsCode: $("#detailsCode").val().trim(),
                            ShapeCode: "",//$("#shapeCode").val().trim(),
                            UnitID: $("#unitid").val(),
                            SaleAttr: attrlist,
                            AttrValue: valuelist,
                            SaleAttrValue: attrvaluelist,
                            Price: $("#detailsPrice").val(),
                            BigPrice: (model.SmallUnitID != model.BigUnitID ? $("#bigPrice").val() : $("#detailsPrice").val()) * model.BigSmallMultiple,
                            Weight: 0,
                            ImgS: _self.ImgS,
                            Description: desc
                        };
                        Global.post("/Products/SavaProductDetail", {
                            product: JSON.stringify(Model)
                        }, function (data) {
                            if (data.ID.length > 0) {
                                Global.post("/Products/GetProductByID", {
                                    productid: model.ProductID,
                                }, function (data) {
                                    _self.getChildList(data.Item);
                                });
                            }
                        });
                    },
                    callback: function () {

                    }
                }
            });

            //绑定单位
            $("#unitName").text(model.SmallUnit.UnitName)
            if (model.SmallUnitID != model.BigUnitID) {
                $("#bigName").text(model.BigUnit.UnitName);
                $("#bigquantity").text(model.BigSmallMultiple);
            } else {
                $("#bigpriceli").hide();
            }

            if (!id) {
                $("#detailsPrice").val(model.Price);
                $("#bigPrice").val(model.Price);
            } else {
                var detailsModel;
                for (var i = 0, j = model.ProductDetails.length; i < j; i++) {
                    if (id == model.ProductDetails[i].ProductDetailID) {
                        detailsModel = model.ProductDetails[i];
                    }
                }
                $("#detailsPrice").val(detailsModel.Price);
                $("#bigPrice").val(detailsModel.BigPrice / model.BigSmallMultiple);
                $("#detailsCode").val(detailsModel.DetailsCode);
                _self.ImgS = detailsModel.ImgS;
                $("#imgS").attr("src", detailsModel.ImgS);

                var list = detailsModel.SaleAttrValue.split(',');
                for (var i = 0, j = list.length; i < j; i++) {
                    $("#" + list[i].split(':')[0]).val(list[i].split(':')[1]).prop("disabled", true);
                }

            }

            ImgsIco = Upload.createUpload({
                element: "#imgSIco",
                buttonText: "选择产品图片",
                className: "edit-Product",
                data: { folder: '/Content/tempfile/', action: 'add', oldPath: _self.ImgS },
                success: function (data, status) {
                    if (data.Items.length > 0) {
                        _self.ImgS = data.Items[0];
                        $("#imgS").attr("src", data.Items[0] + "?" + count++);
                    }
                }
            });

            DetailsVerify = Verify.createVerify({
                element: ".verify",
                emptyAttr: "data-empty",
                verifyType: "data-type",
                regText: "data-text"
            });
        });
    }

    module.exports = Product;
})