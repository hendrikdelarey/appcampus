interface scope {
    pick12HourFormat: any;
    language: any;
    useCurrent: any;
    apply: Function;
    location: any;
    dateTime: any;
    $apply: Function;
    saveAnnouncement: Function;
}

app.directive("reformatdate", function ($rootScope) {
    return {
        link: function (scope, elem, attrs:any) {
            $(elem).on("input", function (e) {
                if ($(this).val().slice(0,-1) != "") {
                    if (new Date($(this).parent().data("DateTimePicker").getDate()).toLocaleString()) {
                        $(this).val(new Date($(this).parent().data("DateTimePicker").getDate()).toLocaleString());
                    }
                } else {
                    $(this).parent().trigger("change");
                    $(this).parent().trigger("dp.change");
                }
                
            });
        }
    }
});

app.directive('datetimepicker', function ($rootScope) {
    var timeout = {};

    return {
        require: '?ngModel',
        restrict: 'AE',
        scope: {
            pick12HourFormat: '@',
            language: '@',
            useCurrent: '@',
            location: '@'
        },
        link: function (scope: scope, elem, attrs) {
            $(elem).datetimepicker({
                pick12HourFormat: scope.pick12HourFormat,
                language: scope.language,
                useCurrent: false
            });

            $(".date-selector").on("dp.change", function (e: any) {
                $(this).children("input").trigger("input");
                if ((<HTMLElement>e.currentTarget).id.indexOf("start-date") >= 0) {
                    if (typeof $("#end-date").data("DateTimePicker") != "undefined") {
                        if (typeof (e.date) === "undefined") {
                            e.date = $("#start-date").data("DateTimePicker").getDate();
                        }
                        $("#end-date").data("DateTimePicker").setMinDate(e.date);
                    }
                }
                if ((<HTMLElement>e.currentTarget).id.indexOf("end-date") >= 0) {
                    if (typeof (e.date) === "undefined") {
                        e.date = $("#end-date").data("DateTimePicker").getDate();
                    }
                    $("#start-date").data("DateTimePicker").setMaxDate(e.date);
                }
            });

            //Local event change
            $(".date-selector").on('change', function (e: any) {
                clearTimeout(<any>timeout);
                scope.dateTime = $(e.currentTarget).data("DateTimePicker").getDate();
                // Global change propagation

                $rootScope.$broadcast("emit:dateTimePicker", {
                    location: scope.location,
                    action: 'changed',
                    dateTime: scope.dateTime,
                    example: scope.useCurrent,
                    element: this
                });
                
                scope.$apply();
            });
        }
    };
});