//进行时间运算

//计算时间差的天数
function DaySpan(start, end) {
    if (!_.isDate(start)) {
        start = Date.parse(start);
    }

    if (!_.isDate(end)) {
        end = Date.parse(end);
    }

    //计算时间差
    let span = end - start;

    return parseInt(span / (1000 * 60 * 60 * 24));
}

//计算时间差的小时
function HourSpan(start, end) {
    if (!_.isDate(start)) {
        start = Date.parse(start);
    }

    if (!_.isDate(end)) {
        end = Date.parse(end);
    }

    //计算时间差
    let span = end - start;

    return parseInt(span / (1000 * 60 * 60));
}

//计算时间差的分钟
function MinuteSpan(start, end) {
    if (!_.isDate(start)) {
        start = Date.parse(start);
    }

    if (!_.isDate(end)) {
        end = Date.parse(end);
    }

    //计算时间差
    let span = end - start;

    return parseInt(span / (1000 * 60));
}

//计算时间差的秒数
function SecondSpan(start, end) {
    if (!_.isDate(start)) {
        start = Date.parse(start);
    }

    if (!_.isDate(end)) {
        end = Date.parse(end);
    }

    //计算时间差
    let span = end - start;

    return parseInt(span / (1000));
}