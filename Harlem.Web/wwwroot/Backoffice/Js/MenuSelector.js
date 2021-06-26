
$(function () {
    if (topMenu != undefined) {
        var topElement = $('.nav-sidebar .' + topMenu + " a:first");
        if (topElement.length > 0) {
            $('.nav-sidebar .' + topMenu).toggleClass("menu-open");
            topElement.toggleClass("active");
            if (subMenu != undefined) {
                console.log("SubElement");
                var subElement = $('.nav-sidebar .' + topMenu + " ." + subMenu + " a:first");
                subElement.toggleClass("active");
        }
        }
    }

})
