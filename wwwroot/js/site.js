// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

(function () {
    const storageKey = "hrreserve-theme";
    const root = document.documentElement;

    function normalizeTheme(theme) {
        return theme === "dark" ? "dark" : "light";
    }

    function getSavedTheme() {
        try {
            return normalizeTheme(localStorage.getItem(storageKey));
        } catch (error) {
            return "light";
        }
    }

    function setSavedTheme(theme) {
        try {
            localStorage.setItem(storageKey, theme);
        } catch (error) {
            // Theme persistence is a progressive enhancement.
        }
    }

    function applyTheme(theme) {
        const normalizedTheme = normalizeTheme(theme);
        root.setAttribute("data-theme", normalizedTheme);

        if (document.body) {
            document.body.setAttribute("data-theme", normalizedTheme);
        }

        const toggle = document.getElementById("themeToggle");
        if (!toggle) {
            return;
        }

        const isDark = normalizedTheme === "dark";
        const icon = toggle.querySelector(".theme-toggle-icon");
        const text = toggle.querySelector(".theme-toggle-text");

        toggle.setAttribute("aria-pressed", isDark.toString());
        if (icon) {
            icon.textContent = isDark ? "☀️" : "🌙";
        }
        if (text) {
            text.textContent = isDark ? "Світла" : "Темна";
        }
    }

    applyTheme(getSavedTheme());

    document.addEventListener("DOMContentLoaded", function () {
        applyTheme(getSavedTheme());

        const toggle = document.getElementById("themeToggle");
        if (!toggle) {
            return;
        }

        toggle.addEventListener("click", function () {
            const nextTheme = root.getAttribute("data-theme") === "dark" ? "light" : "dark";
            setSavedTheme(nextTheme);
            applyTheme(nextTheme);
        });
    });
})();
