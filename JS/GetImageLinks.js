var imgElements = document.getElementsByTagName('article')[0].querySelectorAll('img');
var imgSrcArray = [];
for(var i = 0; i < imgElements.length;i++) {
    imgSrcArray.push(imgElements[i].getAttribute('src'));
}

imgSrcArray.toString();

