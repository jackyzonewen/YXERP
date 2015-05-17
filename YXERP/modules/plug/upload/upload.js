
define(function (require, exports, module) {
    var $ = require("jquery");

    require("plug/upload/jquery.form.js");
    var Defaults = {
        element: "#",		        //元素ID
        buttonText: "上传",         //按钮文本
        url: "/Plug/UploadFile",	//文件临时存储路径
        data: {},
        className: "",
        beforeSubmit: function () { },
        error: function () { },
        success: function () { },		//上传成功
    };

    var UPLoad = function (options) {
        var _self = this;
        _self.setting = $.extend({}, Defaults, options);
        _self.init();
    };
    UPLoad.prototype.init = function () {
        var _self = this;
        if (_self.setting.element) {
            var form = $('<form id="' + _self.setting.element + '_postForm" enctype="multipart/form-data"></form>'),
                file = $('<input type="file" name="file" id="' + _self.setting.element + '_fileUpLoad" style="display:none;" />'),
                button = $('<input id="' + _self.setting.element + '_buttonSubmit" class="' + _self.setting.className + '" type="button" value="' + _self.setting.buttonText + '" />')
            form.append(file).append(button);

            $(_self.setting.element).append(form);

            form.submit(function () {
                var options = {
                    target: _self.setting.target,
                    url: _self.setting.url,
                    type: "post",
                    data: _self.setting.data,
                    beforeSubmit: null,
                    error: _self.setting.error(),
                    success: _self.setting.success
                };
                $(this).ajaxSubmit(options);
                return false;
            })
            button.click(function () {
                file.click();
            });
            file.change(function () {
                form.submit();
            })
        }
    };
    exports.createUpload = function (options) {
        return new UPLoad(options);
    }
});
