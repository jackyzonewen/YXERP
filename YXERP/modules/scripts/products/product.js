
define(function (require, exports, module) {
    var Upload = require("upload"), ProductIco,
        Global = require("global"),
        Verify = require("verify"), VerifyObject, editor,
        doT = require("dot");
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
                if (!_self.ProductID && confirm("产品添加成功，是否继续设置子产品？")) {
                    location.href = "/Products/ProductDetails/" + data.ID;
                } else {
                    location.href = "/Products/ProductList";
                }
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
    }

    module.exports = Product;
})