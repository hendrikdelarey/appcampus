app.directive("activateKnob", ["$timeout", function ($timeout) {
    return {
        link: function ($scope, element, attrs) {
            setTimeout(function () {
                $(".cpu").knob({ "width": 70});
                $(".disk-space").knob({ "width": 70 });
                $(".memory").knob({ "width": 70 });
            }, 0);
        }
    }
}]); 