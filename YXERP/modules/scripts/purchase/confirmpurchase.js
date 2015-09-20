
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

        $("#btnconfirm").click(function () {
            _self.submitOrder();
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
    //更改数量
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
    ObjectJS.submitOrder = function () {
        var _self = this;
        var totalamount = 0, list = [];
        //单据明细
        $(".cart-item").each(function () {
            var _this = $(this);
            var model = {
                AutoID: _this.data("autoid"),
                ProductDetailID: _this.data("id"),
                Quantity: _this.find(".quantity").val(),
                Price: _this.find(".price").val(),
                BatchCode: _this.find(".batch").val(),
                TotalMoney: _this.find(".quantity").val() * _this.find(".price").val()
            };
            list.push(model);
            totalamount += model.TotalMoney;
        });
        if (list.length <= 0) {
            return;
        }
        var entity = {
            TotalMoney: totalamount,
            Remark: $("#remark").val(),
            Details: list
        };
        Global.post("/Purchase/SubmitPurchase", { doc: JSON.stringify(entity) }, function (data) {
            if (data.ID.length > 0) {
                location.href = "/Purchase/MyPurchase";
            }
        })
    }

    module.exports = ObjectJS;
})