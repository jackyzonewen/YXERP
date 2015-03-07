
/*基础配置*/
seajs.config({
    base: "/modules/",
    alias: {
        "jquery": "/Scripts/jquery-1.11.1.js",
        "global": "scripts/global.js",
        "verify": "plug/verify.js"
    }
});


seajs.config({
    alias: {
        "layout-js": "manage/scripts/manage-layout.js"
    }
});

