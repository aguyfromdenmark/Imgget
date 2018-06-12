var imgSrcArray = [];
var interval = setInterval(function () {
    window.scrollTo(0, document.body.scrollHeight);
    var imgElements = document.getElementsByTagName('article')[0].querySelectorAll('img');
    for (var i = 0; i < imgElements.length; i++) {
        imgSrcArray.push(imgElements[i].getAttribute('src'));
    }
}, 300);

var uniqueArray = imgSrcArray.filter(function(item, pos) {
    return imgSrcArray.indexOf(item) == pos;
});
uniqueArray.toString();