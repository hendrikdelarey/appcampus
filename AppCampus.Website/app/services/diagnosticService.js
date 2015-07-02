/// <reference path="../app.ts" />
app.factory("diagnosticService", function ($resource, userService, ENV) {
    var requestUri = ENV.apiUrl + "/api/v1/companies/:companyId/signboards/:signboardId/";
    return $resource(requestUri, { companyId: function () { return userService.getCompanyId(); } }, {
        "latest": {
            method: "GET",
            params: { signboardId: "@signboardId" },
            url: requestUri + "diagnostics/latest"
        }
    });
});
