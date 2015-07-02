/// <reference path="../app.ts" />
app.factory("devicesService", function ($resource, userService, ENV) {
    var requestUri = ENV.apiUrl + "/api/v1/companies/:companyId/devices/:action";
    return $resource(requestUri, { companyId: function () { return userService.getCompanyId(); } }, {
        declined: { method: "POST" },
        blocked: { method: "POST" }
    });
});
