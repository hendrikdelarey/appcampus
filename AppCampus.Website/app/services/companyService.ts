app.factory("companyService", function ($resource, userService, ENV) {
    var requestUri = ENV.apiUrl + "/api/v1/companies";
    return $resource(requestUri, { },
        {
            "get": {
                method: "GET",
                params: {
                    companyId: function () { return userService.getCompanyId() }
                },
                url: requestUri + "/:companyId"
            },
            "delete": {
                method: "DELETE",
                params: {
                    companyId: "@companyId"
                },
                url: requestUri + "/:companyId"
            }
        });
});
 