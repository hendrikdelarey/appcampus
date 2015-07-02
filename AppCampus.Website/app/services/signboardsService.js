/// <reference path="../app.ts" />
app.factory("signboardsService", function ($resource, userService, ENV) {
    var requestUri = ENV.apiUrl + "/api/v1/companies/:companyId/signboards/:signboardId";
    return $resource(requestUri, { companyId: function () { return userService.getCompanyId(); } }, {
        save: { method: "POST" },
        delete: { method: "POST" },
        request: {
            method: 'POST',
            params: { signboardId: "@signboardId" },
            url: requestUri + "/requests",
            transformResponse: function (data) {
                return { list: angular.fromJson(data) };
            }
        }
    });
});
