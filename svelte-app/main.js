import App from './App.svelte';

window.addEventListener("load", () => {
    if ("serviceWorker" in navigator) {
        navigator.serviceWorker.register("service-worker.js");
    }
});

const app = new App({
	target: document.body,
	props: {}
});

export default app;