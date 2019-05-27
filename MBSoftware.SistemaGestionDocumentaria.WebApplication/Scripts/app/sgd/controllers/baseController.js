if (!jQuery) { throw new Error("baseModule.js requires jQuery"); }

+function ($) {
    "use strict";
}(window.jQuery);

var baseModule = (function () {

    var add = function (url, parameters) {
        var dfd = jQuery.Deferred();

        jQuery.ajax({
            type: "POST",
            url: url,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(parameters),
            async: true,
            cache: false,
            success: function (data) {
                dfd.resolve(data);
            },
            error: function (data, error) {
                dfd.reject(data, error);
            }
        });

        return dfd.promise();
    };

    var get = function (url, parameters) {
        var dfd = jQuery.Deferred();

        jQuery.ajax({
            type: "GET",
            url: url +"/"+parameters,
            async: true,
            cache: false,
            success: function (data) {
                dfd.resolve(data);
            },
            error: function (data, error) {
                dfd.reject(data, error);
            }
        });

        return dfd.promise();
    };

    var getLocalStorage = function (url, parametros) {
        var dfd = jQuery.Deferred();

        var params = [];
        var paramsApi = [];

        parametros.forEach(function (entry) {
            var parametros = localStorage.getItem(entry.key);
            if (parametros != null) 
                params.push({ key: entry.key, data: JSON.parse(parametros) });
            else
                paramsApi.push(entry.idParametro);
        });

        if (paramsApi.length == 0)
        {
            dfd.resolve(params);
            return dfd.promise();
        }
        
        jQuery.ajax({
            type: "POST",
            url: url,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(paramsApi),
            async: true,
            cache: false,
            success: function (data) {
                data.forEach(function (entry) {
                    localStorage.setItem(entry.key, JSON.stringify(entry.data));
                });
                dfd.resolve(data);
            },
            error: function (data, error) {
                dfd.reject(data, error);
            }
        });

        return dfd.promise();
    };

    return {
        Add: add,
        Get: get,
        GetLocalStorage: getLocalStorage
    }
})();