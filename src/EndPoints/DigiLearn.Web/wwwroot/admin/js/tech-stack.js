function getQueryStringParameters() {
    var n = "",
        r = "";
    if (sessionStorage.getItem("isp", r)) (n = sessionStorage.getItem("BuyNowLink")), (r = sessionStorage.getItem("isp"));
    else
        try {
            var e = window.location.search;
            1 == new URLSearchParams(e).get("isp") ? ((r = "?isp=1"), (n = "https://1.envato.market/baeyGk"), sessionStorage.setItem("isp", r), sessionStorage.setItem("BuyNowLink", n)) : (n = "https://1.envato.market/zNkqj6");
        } catch (e) {
            n = "https://1.envato.market/zNkqj6";
        }
    document.addEventListener("DOMContentLoaded", function () {
        for (var e = document.querySelectorAll(".btn-buy, .buynowlinks"), t = 0; t < e.length; t++) e[t].setAttribute("href", n);
    }),
        document.addEventListener("DOMContentLoaded", function () {
            for (var e = document.querySelectorAll(".technology-block a,.drp-technology a, .tech-link a"), t = 0; t < e.length; t++) {
                var n = e[t].getAttribute("href");
                e[t].setAttribute("href", n + r);
            }
        });
}
getQueryStringParameters();
