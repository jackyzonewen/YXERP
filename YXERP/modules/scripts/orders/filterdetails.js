
define(function (require, exports, module) {
    var Global = require("global"),
        doT = require("dot"),
        Easydialog = require("easydialog");

    require("cart");

    var ObjectJS = {};
    //添加页初始化
    ObjectJS.init = function (model, detailid, ordertype) {
        var _self = this;
        _self.detailid = detailid;
        _self.ordertype = ordertype;
        model = JSON.parse(model.replace(/&quot;/g, '"'));
        
        _self.bindDetail(model);
        _self.bindEvent(model);

        $(".content-body").createCart(ordertype);
        
    }
    //绑定事件
    ObjectJS.bindEvent = function (model) {
        var _self = this;

        //选择规格
        $("#saleattr li.value").click(function () {
            var _this = $(this);
            if (!_this.hasClass("hover")) {
                _this.addClass("hover");
                _this.siblings().removeClass("hover");
                for (var i = 0, j = model.ProductDetails.length; i < j; i++) {

                    var bl = true, vales = model.ProductDetails[i].AttrValue, unitid = model.ProductDetails[i].UnitID;
                    $(".salesattr li.hover").each(function () {
                        if (vales.indexOf($(this).data("id")) < 0) {
                            bl = false;
                        }
                    });

                    if (bl) {
                        $("#addcart").prop("disabled", false).removeClass("addcartun");
                        _self.detailid = model.ProductDetails[i].ProductDetailID;
                        if ($("#small").hasClass("hover")) {
                            $("#price").html("￥" + model.ProductDetails[i].Price.toFixed(2));
                        } else {
                            $("#price").html("￥" + model.ProductDetails[i].BigPrice.toFixed(2));
                        }
                        $("#productimg").attr("src", model.ProductDetails[i].ImgS);
                        return;
                    } else {
                        $("#addcart").prop("disabled", true).addClass("addcartun");
                    }
                }
            }
        });

        //产品数量
        $("#quantity").blur(function () {
            if (!$(this).val().isInt()) {
                $(this).val("1");
            } else if ($(this).val() < 1) {
                $(this).val("1");
            }
        });
        //+1
        $("#quantityadd").click(function () {
            $("#quantity").val($("#quantity").val() * 1 + 1);
        });
        //-1
        $("#quantityreduce").click(function () {
            if ($("#quantity").val() != "1") {
                $("#quantity").val($("#quantity").val() * 1 - 1);
            }
        });

        //加入购物车
        $("#addcart").click(function () {
            var cart = $("#shopping-cart").offset();
            var temp = $("<div style='width:30px;height:30px;'><img style='width:30px;height:30px;' src='" + $("#productimg").attr("src") + "' /></div>");
            temp.offset({ top: $(this).offset().top + 20, left: $(this).offset().left + 100 });
            temp.css("position", "absolute");
            $("body").append(temp);
            temp.animate({ top: cart.top, left: cart.left }, 500, function () {
                temp.remove();
                var remark = "";
                $("#saleattr ul.salesattr").each(function () {
                    var _this = $(this);
                    remark += "[" + _this.find(".attrkey").html() + _this.find("li.hover").html() + "]";
                });
                Global.post("/Orders/AddShoppingCart", {
                    productid: _self.productid,
                    detailsid: _self.detailid,
                    quantity: $("#quantity").val(),
                    unitid: $("#unit li.hover").data("id"),
                    isBigUnit: $("#unit li.hover").data("value"),
                    ordertype: _self.ordertype,
                    remark: remark
                }, function (data) {
                    if (data.Status) {
                        $("#quantity").val("1");
                        $("#shopping-cart .totalcount").html($("#shopping-cart .totalcount").html() * 1 + 1);
                    }
                });
            });
        });

    }
    //绑定信息
    ObjectJS.bindDetail = function (model) {
        var _self = this;
        _self.productid = model.ProductID;
        //绑定子产品详情
        for (var i = 0, j = model.ProductDetails.length; i < j; i++) {
            if (model.ProductDetails[i].ProductDetailID == _self.detailid) {
                var list = model.ProductDetails[i].SaleAttrValue.split(",");
                for (var ii = 0, jj = list.length; ii < jj; ii++) {
                    var item = list[ii].split(":");
                    $(".attr-item[data-id='" + item[0] + "']").find("li[data-id='" + item[1] + "']").addClass("hover");
                }
                $("#price").html("￥" + model.ProductDetails[i].Price.toFixed(2));
                $("#productimg").attr("src", model.ProductDetails[i].ImgS);
                break;
            }
        }
        $("#description").html(decodeURI(model.Description));
    }

    module.exports = ObjectJS;
})