import { writable } from "svelte/store";

export const currentPage = writable("");

export const notifications = writable([]);

export const priority = writable(-1);
export const soundType = writable("");
export const notification_count = writable(10);
