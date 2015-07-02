/// <reference path="../app.ts" />
app.factory("softwareService", function ($resource, userService, ENV) {
    var requestUri = ENV.apiUrl + "/api/v1/software/:softwareId";
    return $resource(
        requestUri,
        {},
        {
            "uploadFile": {
                method: "POST",
                params: { softwareId: "@softwareId" },
                url: requestUri + "/file",
                //transformRequest: angular.identity,
                headers: { "Content-Type": undefined }
            },
            "downloadFile": {
                method: "GET",
                params: { softwareId: "@softwareId" },
                url: requestUri + "/file"
            }
        });
});
 