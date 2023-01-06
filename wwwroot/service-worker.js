"use strict";
// Cache Name
const CACHE_NAME = "offline";

self.addEventListener("install", (event) => {
    event.waitUntil(
        (async () => {
            const cache = await caches.open(CACHE_NAME);
            return await cache.addAll(
                [
                    "/",
                    "/images/icon.png",
                    "https://fonts.googleapis.com/icon?family=Material+Icons",
                    "/global.css",
                    "/build/bundle.css",
                    "/build/bundle.js"
                ].map((url) => new Request(url, { cache: "reload" }))
            );
        })()
    );
    self.skipWaiting();
});
// Active PWA Cache and clear out anything older
self.addEventListener("activate", (evt) => {
  evt.waitUntil(
    caches.keys().then((keyList) => {
      return Promise.all(
        keyList.map((key) => {
          if (key !== CACHE_NAME) {
            return caches.delete(key);
          }
        })
      );
    })
  );
  self.clients.claim();
});
// listen for fetch events in page navigation and return anything that has been cached
self.addEventListener("fetch", (evt) => {
  // when not a navigation event return
  if (evt.request.mode !== "navigate") {
    return;
  }
  evt.respondWith(
    fetch(evt.request).catch(() => {
      return caches.open(CACHE_NAME).then((cache) => {
        return cache.match("offline.html");
      });
    })
  );
});