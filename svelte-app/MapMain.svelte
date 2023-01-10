<script >
import L from "leaflet";
import { onMount } from 'svelte';
import { redIcon, orangeIcon, yellowIcon, greyIcon, blueIcon} from './leaflet-color-markers'
import { notifications, changedNotifications } from "./stores";

let array = []
let arrayPins = []
let mapDict = {};
let statusDict = {};
let marker 
let mapContainer;

const returnNada = () => '';
let map = L.map(L.DomUtil.create("div"), {
    center: [0, 0],
    zoom: 0,
}).setView([-0.6955588075932391, 22.583414923387544],7);

L.tileLayer('https://server.arcgisonline.com/ArcGIS/rest/services/World_Imagery/MapServer/tile/{z}/{y}/{x}').addTo(map);

onMount(() => {
    mapContainer.appendChild(map.getContainer());
    map.getContainer().style.width = "100%";
    map.getContainer().style.height = "100%";
    map.getContainer().style.class = "rounded-lg";
    map.invalidateSize();
    document.getElementsByClassName( 'leaflet-control-attribution' )[0].style.display = 'none';
    document.getElementsByClassName( 'leaflet-control-zoom' )[0].style.display = 'none';
});

export function flyToLocation(Latitude,Longitude){
    map.flyTo([Latitude, Longitude],9);
}
function removeAllLocations(){
    if (arrayPins){
        arrayPins.forEach(element => {
            map.removeLayer(element)
        });
    }
    array = []
    arrayPins = []
    mapDict = {};
    statusDict = {};
    $changedNotifications = false
    
}
function checkStatus(status){
    switch (status) {
        case 1:
            return "Not handled"
        case 2:
            return "Being handled"
        case 3:
            return "Handled"
        case 4:
            return "False alarm"
        default:
            return "Not handled"
    }
}
function checkColor(functionColor){
    switch (functionColor) {
        case "gunshot":
            return redIcon
        case "animal":
            return yellowIcon
        case "vehicle":
            return orangeIcon
        case "thunder":
            return blueIcon
        default:
            return greyIcon
    }
}
</script>

<style>
    .map {
        height: 100%;
        width: 100%;
    }
</style>
<div class="map rounded-lg" bind:this="{mapContainer}"></div>

    {#if $changedNotifications}
        {removeAllLocations()}
    {/if}
    {#if $notifications != null}
        {#each $notifications as notification}
            {#if array.includes(notification.guid) && statusDict[notification.guid] != checkStatus(notification.statusID)}
                {returnNada(mapDict[notification.guid].getPopup().setContent(notification.sound_type+" with probablity of "+notification.probability+"% status is "+checkStatus(notification.statusID)).update())}
                {returnNada(statusDict[notification.guid] = checkStatus(notification.statusID))}
            {/if}
            {#if !array.includes(notification.guid)}
                {returnNada(marker = (L.marker([notification.latitude, notification.longitude],{icon: checkColor(notification.sound_type)}).bindPopup(notification.sound_type+" with probablity of "+notification.probability+"% status is "+checkStatus(notification.statusID))).addTo(map))}
                {returnNada(arrayPins.push(marker))}
                {returnNada(array.push(notification.guid))}
                {returnNada(mapDict[notification.guid] = marker)}
                {returnNada(statusDict[notification.guid] = checkStatus(notification.statusID))}
            {/if}
        {/each}
    {/if}



