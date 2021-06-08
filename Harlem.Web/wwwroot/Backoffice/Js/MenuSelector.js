
$(function () {
    if (topMenu != undefined) {

        console.log(topMenu)
        var topElement = $('.nav-sidebar .' + topMenu + " a:first");
        console.log("ToplElemnt");
        console.log(topElement);
        if (topElement.length > 0) {
            $('.nav-sidebar .' + topMenu).toggleClass("menu-open");
            topElement.toggleClass("active");
            if (subMenu != undefined) {
                console.log(subMenu);
                console.log("SubElement");
                var subElement = $('.nav-sidebar .' + topMenu + " ." + subMenu + " a:first");
                console.log(subElement);

                subElement.toggleClass("active");
        }
        }
    }

})
