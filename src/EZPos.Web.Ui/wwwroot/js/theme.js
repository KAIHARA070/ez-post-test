window.ezTheme = {
    set: function (mode) {
        document.documentElement.setAttribute("data-theme", mode);
        localStorage.setItem("ezpos-theme", mode);
    },
    applySaved: function () {
        const saved = localStorage.getItem("ezpos-theme");
        const mode = saved || "dark";
        document.documentElement.setAttribute("data-theme", mode);
        return mode === "dark";
    }
};
