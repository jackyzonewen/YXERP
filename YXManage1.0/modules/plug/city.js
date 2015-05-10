/*
--省市县插件--
--引用
var city = require("city");
--实例化
var city = areaCity.createCity({
    elementID: "" //父块状元素ID
});
city.getCityCode(); 获取选择地区编码
city.getName(); 获取选择地区名称 返回{ Province:'' ,City":'', County:''}
city.setValue(cityCode); 设置选中地区
*/
define(function (require, exports, module) {

    var $ = require("jquery"), Global = require("global"), Option = "<option value=''>请选择</option>";
    var Default = {
        elementID: "cityPlug",
        dataUrl: "/Plug/GetCityByPCode"
    };
    var City = function (options) {
        this.setting = [];
        this.init(options);
    };
    //初始化
    City.prototype.init = function (options) {
        var _self = this, _element;
        _self.setting = $.extend([], Default, options);
        _element = $("#" + _self.setting.elementID)
        _self.province = $("<select></select>", { id: _self.setting.elementID + "_province", "class": "" }).append(Option);
        _self.city = $("<select></select>", { id: _self.setting.elementID + "_city", "class": "" }).append(Option);
        _self.county = $("<select></select>", { id: _self.setting.elementID + "_county", "class": "" }).append(Option);
        _element.append(_self.province).append(_self.city).append(_self.county);
        _self.getChildren(_self.province, "CHN");
        _self.bindEvent();
    }
    //绑定事件
    City.prototype.bindEvent = function () {
        var _self = this;
        _self.province.on("change", function () {
            _self.city.empty();
            _self.city.append(Option);
            _self.county.empty();
            _self.county.append(Option);
            if ($(this).val() != "") {
                _self.getChildren(_self.city, $(this).val());
            }
        });
        _self.city.on("change", function () {
            _self.county.empty();
            _self.county.append(Option);
            if ($(this).val() != "") {
                _self.getChildren(_self.county, $(this).val());
            }
        });
    }

    //获取下级区域列表
    City.prototype.getChildren = function (element, cityCode, callback) {
        var _self = this;
        Global.post(_self.setting.dataUrl, { cityCode: cityCode }, function (data) {
            var _length = data.Items.length;
            for (var i = 0; i < _length; i++) {
                element.append("<option value=" + data.Items[i].CityCode + ">" + data.Items[i].Name + "</option>");
            }
            if (!!callback && typeof callback === "function")
                callback();
        });
    }

    //获取选择地区编码
    City.prototype.getCityCode = function () {
        var _self = this;
        if (_self.county.val() != "") {
            return _self.county.val();
        } else if (_self.city.val() != "") {
            return _self.city.val();
        } else if (_self.province.val() != "") {
            return _self.province.val();
        } else {
            return "";
        }
    }
    //获取地区名称
    City.prototype.getName = function () {
        var _self = this,
        _province = _self.province.find("option:selected").text(),
        _city = _self.city.find("option:selected").text(),
        _county = _self.county.find("option:selected").text();
        return {
            Province: _province == "请选择" ? "" : _province,
            City: _city == "请选择" ? "" : _city,
            County: _county == "请选择" ? "" : _county
        }
    }
    //获取地区名称
    City.prototype.setValue = function (cityCode) {
        if (cityCode.length != 6) {
            return;
        }
        var _self = this, _province = cityCode.substr(0, 2), _city = cityCode.substr(2, 2), _county = cityCode.substr(4, 2);
        var province = _self.province.find("option[value^='" + _province + "']");
        province.prop("selected", "selected");
        if (_city == "00") {
            _self.province.change();
            return;
        }
        _self.getChildren(_self.city, province.val(), function () {
            var city = _self.city.find("option[value*='" + _city + "']");
            city.prop("selected", "selected");
            if (_county == "00") {
                _self.city.change();
                return;
            }
            _self.getChildren(_self.county, city.val(), function () {
                _self.county.find("option[value='" + cityCode + "']").prop("selected", "selected");
            });
        });
    }
    exports.createCity = function (options) {
        return new City(options);
    }
});
