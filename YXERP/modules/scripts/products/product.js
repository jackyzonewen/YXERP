
define(function (require, exports, module) {
    var City = require("city"), ProductCity,
        Upload = require("upload"), ProductIco,
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
    Product.init = function (KindEditor) {
        editor = KindEditor;
        Product.bindEvent();
    }
    //绑定事件
    Product.bindEvent = function () {
        var _self = this;
        ProductCity = City.createCity({ elementID: "ProductCity" });
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
            Description: encodeURI(editor.html())
        };
        Global.post("/Products/SavaProduct", {
            product: JSON.stringify(Product)
        }, function (data) {
            if (data.ID.length > 0) {
                location.href = "/Products/ProductList";
            }
        })
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
    //获取品牌列表
    Product.getList = function () {
        var _self = this;
        $("#Product-items").nextAll().remove();
        Global.post("/Products/GetProductList", Params, function (data) {
            doT.exec("template/products/product_list.html", function (templateFun) {
                var innerText = templateFun(data.Items);
                innerText = $(innerText);
                $("#product-items").after(innerText);

                //绑定启用插件
                innerText.find(".status").switch({
                    open_title: "点击启用",
                    close_title: "点击禁用",
                    value_key: "value",
                    change: function (data,callback) {
                        _self.editStatus(data, data.data("id"), data.data("value"), callback);
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
    //更改品牌状态
    Product.editStatus = function (obj, id, status, callback) {
        var _self = this;
        Global.post("/Products/UpdateProductStatus", {
            ProductID: id,
            status: status ? 0 : 1
        }, function (data) {
            !!callback && callback(data.Status);
        });
    }
    //初始化编辑页数据
    Product.initEdit = function (model) {
        var _self = this;
        model = JSON.parse(model.replace(/&quot;/g, '"'));
        _self.bindDetailEvent(model);
        _self.bindDetail(model);
    }
    //获取详细信息
    Product.bindDetail = function (model) {
        var _self = this;
        _self.ProductID = model.ProductID;
        $("#ProductName").val(model.Name);
        $("#anotherName").val(model.AnotherName);
        $("#ProductStyle").val(model.ProductStyle);
        if (model.Status == 1) {
            $("#ProductStatus").prop("checked", "checked");
        }
        $("#description").val(model.Remark);
        $("#ProductImg").attr("src", model.IcoPath);
        
        _self.IcoPath = model.IcoPath;
        
        
    }

    Product.bindDetailEvent = function (model) {
        var _self = this, count = 1;
        ProductCity = City.createCity({
            elementID: "ProductCity",
            cityCode: model.CityCode
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

        ProductIco = Upload.createUpload({
            element: "#ProductIco",
            buttonText: "更换商标",
            className: "edit-Product",
            data: { folder: '/Content/tempfile/', action: 'add', oldPath: model.IcoPath },
            success: function (data, status) {
                if (data.Items.length > 0) {
                    _self.IcoPath = data.Items[0];
                    $("#ProductImg").attr("src", data.Items[0] + "?" + count++);
                }
            }
        });
    }

    module.exports = Product;
})