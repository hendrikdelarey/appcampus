app.factory("dateFormatter", function () {
    function formatDate(date: any) {
        if (typeof date === "string") {
            if (date.slice(-1).indexOf("Z") === 0) {
                date = date.slice(0, -1);
            }
            var retDate = new Date(date + "Z");
            return retDate.getFullYear() + "-" + ((retDate.getMonth().toString().length == 1) ? "0" : "") + (retDate.getMonth() + 1) + "-"
                + ((retDate.getDate().toString().length == 1) ? "0" : "") + retDate.getDate() + " "
                + ((retDate.getHours().toString().length == 1) ? "0" : "") + retDate.getHours() + ":"
                + ((retDate.getMinutes().toString().length == 1) ? "0" : "") + retDate.getMinutes();
        } else {
            return new Date(date).toLocaleString();
        }
    };

    return {
        formatDate: formatDate
    }
}); 