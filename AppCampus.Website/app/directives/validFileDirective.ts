app.directive("validFile", function () {
    return {
        require: "ngModel",
        link: function (scope, el:any, attrs, ngModel) {
            el.bind("change", function () {
                scope.$apply(function () {
                    ngModel.$setViewValue(el[0].files[0]);
                    ngModel.$render();
                });
            });
        }
    }
}); 