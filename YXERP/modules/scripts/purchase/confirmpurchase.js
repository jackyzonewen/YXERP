
define(function (require, exports, module) {
    var City = require("city"), CityObj,
        Global = require("global");

    require("switch");

    var ObjectJS = {};
    //添加页初始化
    ObjectJS.init = function () {
        ObjectJS.bindEvent();
        ObjectJS.getAmount();
    }
    //绑定事件
    ObjectJS.bindEvent = function () {
        var _self = this;

        $("#btnSave").on("click", function () {
            if (!VerifyObject.isPass()) {
                return;
            }
            _self.savaEntity();
        });

        //编辑数量
        $(".quantity").change(function () {
            if ($(this).val().isInt() && $(this).val() > 0) {
                _self.editQuantity($(this));
            } else {
                $(this).val($(this).data("value"));
            }
        });
        //编辑单价
        $(".price").change(function () {
            var _this = $(this);
            if (_this.val().isInt() && _this.val() > 0) {
                _this.parent().nextAll(".amount").html((_this.parent().nextAll(".tr-quantity").find("input").val() * _this.val()).toFixed(2));
                _this.data("value", _this.val());
                _self.getAmount();
            } else {
                _this.val(_this.data("value"));
            }
        });
        //删除产品
        $(".ico-del").click(function () {
            var _this = $(this);
            if (confirm("确认从购物车移除此产品吗？")) {
                Global.post("/Orders/DeleteCart", {
                    autoid: _this.data("id")
                }, function (data) {
                    if (!data.Status) {
                        alert("系统异常，请重新操作！");
                    } else {
                        _this.parents("tr.item").remove();
                        _self.getAmount();
                    }
                });
            }
        });

    }
    //计算总金额
    ObjectJS.getAmount = function () {
        var amount = 0;
        $(".amount").each(function () {
            var _this = $(this);
            _this.html((_this.prevAll(".tr-quantity").find("input").val() * _this.prevAll(".tr-price").find("input").val()).toFixed(2));
            amount += _this.html() * 1;
        });
        $("#amount").text(amount.toFixed(2));
    }
    //更改仓库状态
    ObjectJS.editQuantity = function (ele) {
        var _self = this;
        Global.post("/Orders/UpdateCartQuantity", {
            autoid: ele.data("id"),
            quantity: ele.val()
        }, function (data) {
            if (!data.Status) {
                ele.val(ele.data("value"));
                alert("系统异常，请重新操作！");
            } else {
                ele.parent().nextAll(".amount").html((ele.parent().prevAll(".tr-price").find("input").val() * ele.val()).toFixed(2));
                ele.data("value", ele.val());
                _self.getAmount();
            }
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

    //初始化编辑页数据
    ObjectJS.initEdit = function (model) {
        var _self = this;
        model = JSON.parse(model.replace(/&quot;/g, '"'));
        _self.bindDetailEvent(model);
        _self.bindDetail(model);
    }
    //获取详细信息
    ObjectJS.bindDetail = function (model) {
        var _self = this;
        _self.wareID = model.WareID;
        $("#warehouseCode").text(model.WareCode);

        $("#warehouseName").val(model.Name);

        $("#shortName").val(model.ShortName);
        if (model.Status == 1) {
            $("#warehouseStatus").prop("checked", "checked");
        }
        $("#description").val(model.Description);

        
    }
    //绑定详情事件
    ObjectJS.bindDetailEvent = function (model) {
        var _self = this;
        CityObj = City.createCity({
            elementID: "warehouseCity",
            cityCode: model.CityCode
        });
        
        $("#btnSave").on("click", function () {
            if (!VerifyObject.isPass()) {
                return;
            }
            _self.savaEntity();
        });

        VerifyObject = Verify.createVerify({
            element: ".verify",
            emptyAttr: "data-empty",
            verifyType: "data-type",
            regText: "data-text"
        });
       
    }

    module.exports = ObjectJS;
})