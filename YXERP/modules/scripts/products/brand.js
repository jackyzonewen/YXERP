
define(function (require, exports, module) {
    var City = require("city"), BrandCity,
        Global = require("global"),
        Verify = require("verify"), VerifyObject,
        doT = require("dot");
    require("pager");
    var Params = {
        keyWords: "",
        pageSize: 20,
        pageIndex: 1,
        totalCount: 0
    };
    var Brand = {};
    //添加页初始化
    Brand.init = function () {
        Brand.bindEvent();
    }
    //绑定事件
    Brand.bindEvent = function () {
        BrandCity = City.createCity({ elementID: "brandCity" });
        $("#btnSaveBrand").on("click", function () {
            if (!VerifyObject.isPass()) {
                return;
            }
            Brand.savaBrand();
        });

        VerifyObject = Verify.createVerify({
            element: ".verify",
            emptyAttr: "data-empty",
            verifyType: "data-type",
            regText: "data-text"
        });
    }
    //保存品牌
    Brand.savaBrand = function () {
        var _self = this;
        var brand = {
            BrandID: _self.brandID,
            Name: $("#brandName").val().trim(),
            AnotherName: $("#anotherName").val().trim(),
            IcoPath: $("#brandImg").attr("src"),
            CountryCode: "0086",
            CityCode: BrandCity.getCityCode(),
            PostCode: "000000",
            Status: $("#brandStatus").prop("checked") ? 1 : 0,
            Remark: $("#description").val(),
            BrandStyle: $("#brandStyle").val().trim()
        };
        Global.post("/Products/SavaBrand", { brand: JSON.stringify(brand) }, function (data) {
            if (data.ID.length > 0) {
                location.href="/Manage/Products/Brand"
            }
        })
    }

    //列表页初始化
    Brand.initList = function () {
        Brand.getList();
        Brand.bindListEvent();
    }
    //绑定列表页事件
    Brand.bindListEvent = function () {
        require.async("search", function () {
            $(".searth-module").searchKeys(function (keyWords) {
                Params.keyWords = keyWords;
                Brand.getList();
            });
        });
    }
    //获取品牌列表
    Brand.getList = function () {
        var _self = this;
        $("#brand-items").nextAll().remove();
        Global.post("/Manage/Products/GetBrandList", Params, function (data) {
            doT.exec("/modules/manage/template/products/brand_list.html", function (templateFun) {
                var innerText = templateFun(data.Items);
                innerText = $(innerText);
                $("#brand-items").after(innerText);

                //删除事件
                $(".ico-del").click(function () {
                    if (confirm("品牌删除后不可恢复,确认删除吗？")) {
                        Global.post("/Manage/Products/DeleteBrand", { brandID: $(this).attr("data-id") }, function (data) {
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
                    Brand.getList();
                }
            });
        });
    }
    //更改品牌状态
    Brand.editStatus = function (obj, id, status, callback) {
        var _self = this;
        Global.post("/Manage/Products/UpdateBrandStatus", {
            brandID: id,
            status: status ? 0 : 1
        }, function (data) {
            !!callback && callback(data.Status);
        });
    }

    //初始化编辑页数据
    Brand.initEdit = function (brandID,callBack) {
        var _self = this;
        _self.brandID = brandID;
        _self.bindEditEvent();
        Global.post("/Manage/Products/GetBrandDetail", { brandID: brandID }, function (data) {
            _self.bindDetail(data.Item, callBack);
        });
    }
    //获取详细信息
    Brand.bindDetail = function (model, callBack) {
        $("#brandName").val(model.Name);
        $("#anotherName").val(model.AnotherName);
        $("#brandStyle").val(model.BrandStyle);
        BrandArea.setValue(model.AreaCode);
        if (model.Status == 1) {
            $("#brandStatus").prop("checked", "checked");
        }
        $("#description").val(model.Remark);
        $("#brandImg").attr("src", model.ImgPath);

        !!callBack && callBack();
    }
    //绑定编辑事件
    Brand.bindEditEvent = function () {
        BrandArea = AreaCity.createArea({ elementID: "brandArea" });
        $("#btnSavebrand").on("click", function () {
            if (!VerifyObject.isPass())
                return;
            Brand.savaBrand();
        });

        VerifyObject = Verify.createVerify({
            element: ".verify",
            emptyAttr: "data-empty",
            verifyType: "data-type",
            regText: "data-text"
        });
    }

    module.exports = Brand;
})