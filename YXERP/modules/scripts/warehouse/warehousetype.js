define(function (require, exports, module) {
    var Global = require("global"),
        Verify = require("verify"), VerifyObject,
        Easydialog = require("easydialog");
    var ObjectEntity = {
        TypeID: "",
        TypeName: "",
        Description: ""
    };

    var ObjectJS = {};
    //初始化
    ObjectJS.init = function () {
        var _self = this;
        _self.bindEvent();
        _self.bindElementEvent($(".obj-item"));
    }

    //绑定事件
    ObjectJS.bindEvent = function () {
        var _self = this;
        $("#addType").click(function () {
            ObjectEntity.TypeID = "";
            ObjectEntity.TypeName = "";
            ObjectEntity.Description = "";
            _self.addObject(function (obj) {
                var item = $('<li class="obj-item" ><label data-id="' + obj.TypeID + '" data-value="' + obj.TypeName + '" title="' + obj.Description + '">' + obj.TypeName + '</label><span data-id="' + obj.TypeID + '" class="ico-delete"></span></li>');
                _self.bindElementEvent(item);
                $(".obj-list").append(item);
            });
        });
        //搜索
        require.async("search", function () {
            $(".searth-module").searchKeys(function (keyWords) {
                if (!!keyWords) {
                    var _obj = $(".obj-list").find("label[data-value*='" + keyWords + "']");
                    _obj.focus();
                    _obj.select();
                    $(".obj-list li").hide();
                    _obj.parent().show();
                } else {
                    $(".obj-list li").show();
                }
            });
        });
    }
    //添加库别
    ObjectJS.addObject = function (callback) {
        var _self = this;
        var html = '<ul class="create-obj">' +
                        '<li><span class="left">名称：</span><input type="text" id="objName" maxlength="10" value="" class="input verify " data-empty="*必填" /></li>' +
                        '<li><span class="left">描述：</span><textarea id="description">' + ObjectEntity.Description + '</textarea></li>' +
                   '</ul>'
        Easydialog.open({
            container: {
                id: "show-add-object",
                header: ObjectEntity.TypeID == "" ? "添加库别" : "编辑库别",
                content: html,
                yesFn: function () {
                    if (!VerifyObject.isPass()) {
                        return false;
                    }
                    ObjectEntity.TypeName = $("#objName").val();
                    ObjectEntity.Description = $("#description").val();
                    _self.saveObject(callback);
                },
                callback: function () {

                }
            }
        });
        
        $("#objName").focus();
        $("#objName").val(ObjectEntity.TypeName);

        VerifyObject = Verify.createVerify({
            element: ".verify",
            emptyAttr: "data-empty",
            verifyType: "data-type",
            regText: "data-text"
        });
    }
    //保存对象
    ObjectJS.saveObject = function (callback) {
        var _self = this;
        Global.post("/Warehouse/SaveWareHouseType", { type: JSON.stringify(ObjectEntity) }, function (data) {
            if (data.ID.length > 0) {
                if (!ObjectEntity.TypeID) {
                    ObjectEntity.TypeID = data.ID;
                }
                !!callback && callback(ObjectEntity);
            } else {
                alert("操作失败,请稍后重试!");
            }
        });
    }

    //附加元素事件
    ObjectJS.bindElementEvent = function (elments) {
        var _self = this;
        //编辑
        elments.find("label").click(function () {
            var _this = $(this);
            ObjectEntity.TypeID = _this.data("id");
            ObjectEntity.TypeName = _this.text();
            ObjectEntity.Description = _this.attr("title");

            _self.addObject(function (obj) {
                _this.text(obj.TypeName);
                _this.attr("title", obj.Description);
                _this.attr("data-value", obj.TypeName);
                console.log(_this.data("value"));
            });
        });
        //删除
        elments.find(".ico-delete").click(function () {
            var _this = $(this);
            if (_this.data("id") != "") {
                if (confirm("删除后不可恢复,确认删除吗？")) {
                    _self.deleteObj(_this.data("id"), function (status) {
                        status && _this.parent().remove();
                    });
                }
            } else {
                _this.parent().remove();
            }
        })
    }
    //删除库别
    ObjectJS.deleteObj = function (id, callback) {
        Global.post("/Warehouse/DeleteWareHouseType", { id: id }, function (data) {
            !!callback && callback(data.Status);
        })
    }

    module.exports = ObjectJS;
});