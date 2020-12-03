/**
 * 格式化日期
 * @param {any} now 要格式化的当前时间
 * @param {any} fmt 要格式化的字符串格式
 */
export const dateFormat = (now, fmt) => {
    var o = {
        "M+": now.getMonth() + 1, //月份
        "d+": now.getDate(), //日
        "h+": now.getHours(), //小时
        "m+": now.getMinutes(), //分
        "s+": now.getSeconds(), //秒
        "q+": Math.floor((now.getMonth() + 3) / 3), //季度
        "S": now.getMilliseconds() //毫秒
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (now.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}

/**
 * 格式化日期
 * @param {any} time 要格式化的日期时间
 */
export const timeFormat = (time) => {
    var d = new Date(time);
    var year = d.getFullYear();
    var month = (d.getMonth() + 1) < 10 ? "0" + (d.getMonth() + 1) : (d.getMonth() + 1);
    var currentDate = d.getDate() < 10 ? "0" + d.getDate() : d.getDate();
    var hours = d.getHours() < 10 ? "0" + d.getHours() : d.getHours();
    var minutes = d.getMinutes() < 10 ? "0" + d.getMinutes() : d.getMinutes();
    var seconds = d.getSeconds() < 10 ? "0" + d.getSeconds() : d.getSeconds();
    return year + '-' + month + '-' + currentDate + ' ' + hours + ':' + minutes + ':' + seconds;
}