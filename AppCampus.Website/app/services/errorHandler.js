app.factory("errorHandler", function ($log, $location, userService, toastr) {
    function handleError(errorData, callback, message) {
        if (typeof (toastr) === "undefined") {
            return false;
        }
        else if (errorData.status !== 401) {
            if (typeof (message) !== "undefined" && errorData.status !== 403) {
                toastr.error(message, "Error");
            }
            else {
                switch (errorData.status) {
                    case 400:
                        toastr.error("Your request was not carried out because there was an error with your request", "Error");
                        break;
                    case 403:
                        toastr.error("You do not have the required permissions to act on this resource");
                        break;
                    case 404:
                        toastr.error("The resource you are acting upon is not available or does not exist");
                        break;
                    case 405:
                        toastr.error("That method is not allowed, error");
                        break;
                    case 415:
                        toastr.error("Unsupported media type");
                        break;
                    case 500:
                        toastr.error("The server encountered an error. If this continues please alert the administrator");
                        break;
                }
            }
        }
        if (typeof (callback) === "function") {
            callback();
        }
    }
    ;
    return {
        handleError: handleError
    };
});
