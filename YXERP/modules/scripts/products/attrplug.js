
/*
--添加属性--
--引用
var attr = require("scripts/products/attrplug");
attr.create({
    attrid:"",
    categoryid:"",
    callback:function(model){}
});
*/
define(function (require, exports, module) {
    var Global = require("global"),
        Verify = require("verify"), VerifyObject,
        doT = require("dot"),
        Easydialog = require("easydialog");
    require("css/products/attrplug.css");
    var Attr = {
        AttrID: "",
        AttrName: "",
        Description: "",
        CategoryID: ""
    }, Default = {
        attrid: "",
        categoryid: "",
        callback: null
    };

    var ObjectJS = function (options) {
        var _self = this;
        _self.init(options);
    }
    //初始化
    ObjectJS.prototype.init = function (options) {
        var _self = this;
        _self.setting = $.extend([], Default, options);
        if (_self.setting.attrid) {
            _self.getAttrDetail(_self.setting.attrid);
        } else {
            Attr.AttrID = "";
            Attr.AttrName = "";
            Attr.Description = "";
            Attr.CategoryID = _self.setting.categoryid;
            _self.addAttr();
        }
       
    }


    ObjectJS.prototype.getAttrDetail = function (attrid) {
        var _self = this;
        Global.post("/Products/GetAttrByID", { attrID: attrid }, function (data) {
            Attr.AttrID = attrid;
            Attr.AttrName = data.Item.AttrName;
            Attr.Description = data.Item.Description;
            Attr.CategoryID = data.Item.CategoryID;
            _self.addAttr();
        });
    }

    //添加属性弹出层
    ObjectJS.prototype.addAttr = function () {
        var _self = this;
        var html = '<ul class="create-attr">' +
                        '<li><span class="left">名称：</span><input type="text" id="attrName" maxlength="10" value="" class="input verify " data-empty=" *必填" /></li>' +
                        '<li><span class="left">描述：</span><textarea id="attrDescription">' + Attr.Description + '</textarea></li>' +
                   '</ul>'
        Easydialog.open({
            container: {
                id: "show-add-attr",
                header: Attr.AttrID == "" ? "添加属性" : "编辑属性",
                content: html,
                yesFn: function () {
                    if (!VerifyObject.isPass()) {
                        return false;
                    }
                    
                    Attr.AttrName = $("#attrName").val();
                    Attr.Description = $("#attrDescription").val();
                    _self.saveAttr();
                },
                callback: function () {
                    
                }
            }
        });
        
        $("#attrName").focus();
        $("#attrName").val(Attr.AttrName);

        VerifyObject = Verify.createVerify({
            element: ".verify",
            emptyAttr: "data-empty",
            verifyType: "data-type",
            regText: "data-text"
        });
    }

    //保存属性
    ObjectJS.prototype.saveAttr = function () {
        var _self = this;
        Global.post("/Products/SaveAttr", { attr: JSON.stringify(Attr) }, function (data) {
            if (data.ID.length > 0) {
                if (!Attr.AttrID) {
                    Attr.AttrID = data.ID;
                    Attr.ValuesStr = "暂无属性值(单击添加)";
                }
                !!_self.setting.callback && _self.setting.callback(Attr);
            } else {
                alert("操作失败,请稍后重试!");
            }
        });
    }
    //对外公布
    exports.init = function (options) {
        return new ObjectJS(options);
    }
});