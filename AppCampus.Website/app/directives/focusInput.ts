app.directive('focusInput', function ($timeout) {
    return {
        scope: { trigger: '@focusInput' },
        link: function (scope, element) {
            scope.$watch('trigger', function (value) {
                    $timeout(function () {
                        element[0].focus();
                    });
            });
        }
    };
});
