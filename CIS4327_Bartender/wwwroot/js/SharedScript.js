// var dotContainer = document.getElementById("dotContainer");
// var frame = document.getElementById("frame");
// var info = document.getElementById("info");
var dotMenu = document.querySelectorAll('[id^="dotMenu-"]');
var frame = document.querySelectorAll('[id^="frame-"]');
var info = document.querySelectorAll('[id^="info-"]');


for (var i = 0; i < dotMenu.length; i++) {
    dotMenu[i].addEventListener("click", (event) => {
        event.stopPropagation();
        for (var j = 0; j < frame.length; j++) {
            
            if (frame[j].style.visibility == "visible") {
                frame[j].style.visibility = "hidden";
                
            }
            else {
                frame[j].style.visibility = "visible";
                
            }
        }
    });
}

/* hide the menu when clicking outside the menu
document.addEventListener("click", (event) => {
    for (var i = 0; i < dotMenu.lenth; i++) {
        if (!frame[i].contains(event.target) && !dotMenu[i].contains(event.target) && !info[i].contains(event.target)) {
            frame.style.visibility = "hidden";
        }
    }
    
});
*/