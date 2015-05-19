
/*基础配置*/
seajs.config({
    base: "/modules/",
    alias: {
        "jquery": "/Scripts/jquery-1.11.1.js",
        "global": "scripts/global.js",
        //HTML模板引擎
        "dot": "plug/doT.js",
        //分页控件
        "pager": "plug/datapager/paginate.js"
    }
});


seajs.config({
    alias: {
        //数据验证
        "verify": "plug/verify.js",
        //城市地区
        "city": "plug/city.js",
        //上传
        "upload": "plug/upload/upload.js",
        //开关插件
        "switch": "plug/switch/switch.js",
        //弹出层插件
        "easydialog": "plug/easydialog/easydialog.js",
        //搜索插件
        "search": "plug/seach_keys/seach_keys.js"
    }
});

