
define(function (require, exports, module) {
    var Upload = require("upload"), ProductIco,
        Global = require("global"),
        Verify = require("verify"), VerifyObject,
        doT = require("dot"),
        Easydialog = require("easydialog");
    require("switch");
    var Params = {
        keyWords: "",
        pageSize: 20,
        pageIndex: 1,
        totalCount: 0
    };
    var ObjectJS = {};
    //添加页初始化
    ObjectJS.init = function (model) {
        var _self = this;
        model = JSON.parse(model.replace(/&quot;/g, '"'));
        _self.getList(model);
        _self.bindEvent(model);
    }
    //绑定事件
    ObjectJS.bindEvent = function (model) {
        var _self = this;

        $("#addDetails").on("click", function () {
            _self.showTemplate(model, "");
        });
    }
    //添加/编辑子产品
    ObjectJS.showTemplate = function (model, id) {
        var _self = this;
        doT.exec("template/products/productdetails_add.html", function (templateFun) {

            var html = templateFun(model.Category.SaleAttrs);

            Easydialog.open({
                container: {
                    id: "productdetails-add-div",
                    header: !id ? "添加子产品" : "编辑子产品",
                    content: html,
                    yesFn: function () {

                        if (!VerifyObject.isPass()) {
                            return false;
                        }

                        var attrlist = "", valuelist = "", attrvaluelist = "";

                        $(".productattr").each(function () {
                            var _this = $(this);
                            attrlist += _this.data("id") + ",";
                            valuelist += _this.find("select").val() + ",";
                            attrvaluelist += _this.data("id") + ":" + _this.find("select").val() + ",";
                        });

                        var Model = {
                            ProductDetailID: id,
                            ProductID: model.ProductID,
                            DetailsCode: $("#productCode").val().trim(),
                            ShapeCode: "",//$("#shapeCode").val().trim(),
                            UnitID: $("#unitid").val(),
                            SaleAttr: attrlist,
                            AttrValue: valuelist,
                            SaleAttrValue: attrvaluelist,
                            Price: $("#price").val(),
                            Weight: 0,
                            ImgS: _self.ProductImage,
                            Description: ""
                        };
                        Global.post("/Products/SavaProductDetail", {
                            product: JSON.stringify(Model)
                        }, function (data) {
                            if (data.ID.length > 0) {
                                location.href = location.href;
                            }
                        });
                    },
                    callback: function () {

                    }
                }
            });

            //绑定单位
            $("#unitid").append('<option value="' + model.SmallUnit.UnitID + '">' + model.SmallUnit.UnitName + '</option> ');
            if (model.SmallUnitID != model.BigUnitID) {
                $("#unitid").append('<option value="' + model.BigUnit.UnitID + '">' + model.BigUnit.UnitName + '</option> ');
            }

            if (!id) {
                $("#price").val(model.Price);
            } else {
                var detailsModel;
                for (var i = 0, j = model.ProductDetails.length; i < j; i++) {
                    if (id == model.ProductDetails[i].ProductDetailID) {
                        detailsModel = model.ProductDetails[i];
                    }
                }
                $("#price").val(detailsModel.Price);
                $("#unitid").val(detailsModel.UnitID).prop("disabled", true);
                $("#productCode").val(detailsModel.DetailsCode).prop("disabled", true);
                _self.ProductImage = detailsModel.ImgS;
                $("#productImg").attr("src", detailsModel.ImgS);
                
                var list = detailsModel.SaleAttrValue.split(',');
                for (var i = 0, j = list.length; i < j; i++) {
                    $("#" + list[i].split(':')[0]).val(list[i].split(':')[1]).prop("disabled", true);
                }

            }


            ProductIco = Upload.createUpload({
                element: "#productIco",
                buttonText: "选择产品图片",
                className: "edit-Product",
                data: { folder: '/Content/tempfile/', action: 'add', oldPath: _self.ProductImage },
                success: function (data, status) {
                    if (data.Items.length > 0) {
                        _self.ProductImage = data.Items[0];
                        $("#productImg").attr("src", data.Items[0]);
                    }
                }
            });

            VerifyObject = Verify.createVerify({
                element: ".verify",
                emptyAttr: "data-empty",
                verifyType: "data-type",
                regText: "data-text"
            });
        });
    }
    //获取列表
    ObjectJS.getList = function (model) {
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
                    _self.editStatus(data, data.data("id"), data.data("value"), callback);
                }
            });

            innerText.find(".ico-edit").click(function () {
                _self.showTemplate(model, $(this).data("id"));
            });
        });
    }
    //更改状态
    ObjectJS.editStatus = function (obj, id, status, callback) {
        var _self = this;
        Global.post("/Products/UpdateProductDetailsStatus", {
            productdetailid: id,
            status: status ? 0 : 1
        }, function (data) {
            !!callback && callback(data.Status);
        });
    }
    module.exports = ObjectJS;
})