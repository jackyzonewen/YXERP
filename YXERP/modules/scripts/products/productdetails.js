
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
    }, Entity = {

    };
    var ObjectJS = {};
    //添加页初始化
    ObjectJS.init = function (model) {
        var _self = this;
        model = JSON.parse(model.replace(/&quot;/g, '"'));
        console.log(model);
        //_self.getList();
        _self.bindEvent(model);
    }
    //绑定事件
    ObjectJS.bindEvent = function (model) {
        var _self = this;

        $("#addDetails").on("click", function () {
            _self.showTemplate(model);
        });
    }
    ObjectJS.showTemplate = function (model) {
        var _self = this;
        doT.exec("template/products/productdetails_add.html", function (templateFun) {

            var html = templateFun(model.SaleAttrs);

            Easydialog.open({
                container: {
                    id: "productdetails-add-div",
                    header: Entity.DetailsID == "" ? "添加子产品" : "编辑子产品",
                    content: html,
                    yesFn: function () {

                        if (!VerifyObject.isPass()) {
                            return false;
                        }

                        var model = {
                            Entity: Category.CategoryID,
                            CategoryCode: "",
                            CategoryName: $("#categoryName").val(),
                            PID: Category.PID,
                            Status: $("#categoryStatus").prop("checked") ? 1 : 0,
                            Description: $("#description").val()
                        };

                        var attrs = "";
                        //$("#attrList .attr-item").each(function () {
                        //    if ($(this).prop("checked")) {
                        //        attrs += $(this).data("id") + ",";
                        //    }
                        //});
                        var saleattrs = "";
                        //$("#saleAttr .attr-item").each(function () {
                        //    if ($(this).prop("checked")) {
                        //        saleattrs += $(this).data("id") + ",";
                        //    }
                        //});
                        _self.saveCategory(model, attrs, saleattrs, callback);
                    },
                    callback: function () {

                    }
                }
            });

            //编辑填充数据
            if (Entity.CategoryID) {
                $("#categoryName").val(Category.CategoryName);
                $("#categoryStatus").prop("checked", Category.Status == 1);
                $("#description").val(Category.Description);
                ////绑定属性
                //$("#attrList .attr-item").each(function () {
                //    var _this = $(this);
                //    _this.prop("checked", Category.AttrList.indexOf(_this.data("id")) >= 0);
                //});
                ////绑定规格
                //$("#saleAttr .attr-item").each(function () {
                //    var _this = $(this);
                //    _this.prop("checked", Category.SaleAttr.indexOf(_this.data("id")) >= 0);
                //});
            }
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

            VerifyObject = Verify.createVerify({
                element: ".verify",
                emptyAttr: "data-empty",
                verifyType: "data-type",
                regText: "data-text"
            });
        });
    }
    //保存
    ObjectJS.savaEntity = function () {
        var _self = this;
        var entity = {
            WareID: _self.wareID,
            Name: $("#warehouseName").val().trim(),
            WareCode: $("#warehouseCode").val().trim(),
            ShortName: $("#shortName").val().trim(),
            CityCode: CityObj.getCityCode(),
            Status: $("#warehouseStatus").prop("checked") ? 1 : 0,
            Description: $("#description").val()
        };
        Global.post("/Warehouse/SaveWareHouse", { ware: JSON.stringify(entity) }, function (data) {
            if (data.ID.length > 0) {
                location.href = "/Warehouse/WareHouse"
            }
        })
    }

    //获取列表
    ObjectJS.getList = function () {
        var _self = this;
        $("#header-items").nextAll().remove();
        Global.post("/Warehouse/GetWareHouses", Params, function (data) {
            doT.exec("template/warehouse/warehouse_list.html", function (templateFun) {
                var innerText = templateFun(data.Items);
                innerText = $(innerText);
                $("#header-items").after(innerText);

                //删除事件
                $(".ico-del").click(function () {
                    if (confirm("删除后不可恢复,确认删除吗？")) {
                        Global.post("/Warehouse/DeleteWareHouse", { id: $(this).attr("data-id") }, function (data) {
                            if (data.Status) {
                                _self.getList();
                            } else {
                                alert("删除失败！");
                            }
                        });
                    }
                });
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
        });
    }
    //更改状态
    ObjectJS.editStatus = function (obj, id, status, callback) {
        var _self = this;
        Global.post("/Warehouse/UpdateWareHouseStatus", {
            id: id,
            status: status ? 0 : 1
        }, function (data) {
            !!callback && callback(data.Status);
        });
    }
    module.exports = ObjectJS;
})