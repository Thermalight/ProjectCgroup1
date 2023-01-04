<script >
import L from "leaflet";
import { onMount } from 'svelte';
import { redIcon, orangeIcon, yellowIcon, greyIcon } from './leaflet-color-markers'

export let notifications;
export let changedNotifications = false

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
    if (arrayPins != null){
        arrayPins.forEach(element => {
            map.removeLayer(element)
        });
    }
    array = []
    arrayPins = []
    mapDict = {};
    statusDict = {};
    changedNotifications = false
    
}
function checkStatus(status){
    if (status == 1){
        return "Not handled"
    }
    else if (status == 2){
        return "Being handled"
    }
    else if (status == 3){

        return "Handled"
    }
    else if (status == 4){
        return "False alarm"
    }
    else{
        return "Not handled"
    }
}
function checkColor(functionColor){
    if (functionColor == "gunshot"){
        return redIcon
    }
    else if (functionColor == "animal"){
        return yellowIcon
    }
    else if (functionColor == "vehicle"){
        return orangeIcon
    }
    else{
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

    {#if changedNotifications}
        {removeAllLocations()}
    {/if}
    {#if notifications != null}
        {#each notifications as notification}
            {#if array.includes(notification.Guid) && statusDict[notification.Guid] != checkStatus(notification.StatusID)}
                {returnNada(mapDict[notification.Guid].getPopup().setContent(notification.sound_type+" with probablity of "+notification.Probability+"% status is "+checkStatus(notification.StatusID)).update())}
                {returnNada(statusDict[notification.Guid] = checkStatus(notification.StatusID))}
            {/if}
            {#if !array.includes(notification.Guid)}
                {returnNada(marker = (L.marker([notification.Latitude, notification.Longitude],{icon: checkColor(notification.sound_type)}).bindPopup(notification.sound_type+" with probablity of "+notification.Probability+"% status is "+checkStatus(notification.StatusID))).addTo(map))}
                {returnNada(arrayPins.push(marker))}
                {returnNada(array.push(notification.Guid))}
                {returnNada(mapDict[notification.Guid] = marker)}
                {returnNada(statusDict[notification.Guid] = checkStatus(notification.StatusID))}
            {/if}
        {/each}
    {/if}



