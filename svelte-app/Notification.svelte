<script>
    import { slide } from 'svelte/transition';
    import Status from './Status.svelte'
    export let mapComponent
    export let event;
    var audio = new Audio(event.sound);
    audio.volume = 0.1
    let open = false
    let play = false
    
    function round(number) {
        return Math.round(number*100000)/100000
    }

    function toggle() {
        open = !open
    }
    function toggleSound() {
        play = !play

        if (play) {
            audio.play();
        } else {
            audio.pause();
            audio.currentTime = 0;
        }
    }
</script>

{#if event != null}
    <div on:click={mapComponent.flyToLocation(event.latitude, event.longitude)}>
        <div on:click={toggle} class="{event.sound_type} p-4 text-white bg-primary-dark mb-2 rounded-lg">
            <div class="content">
                <button 
                on:click={toggleSound} 
                class="border-transparent focus:border-transparent focus:ring-0">
                        <span class="material-symbols-outlined">{ play ? "pause_circle" : "play_circle" }</span>
                </button>

                <p class="font-bold">{event.sound_type}</p>
                <Status status={event.statusID} GUID={event.guid}/>
                <p class="text-gray-400">Probability: {event.probability}%</p>
                <p class="text-gray-400">Node: {event.nodeID}</p>
                {#if open}
                    <div transition:slide>
                        <p class="text-gray-400">latitude: {round(event.latitude)}</p>
                        <p class="text-gray-400">longitude: {round(event.longitude)}</p>
                    </div>
                {/if}
                <span transition:slide class="absolute left-1/2 pt-3 material-symbols-outlined">{ open? "expand_less" : "expand_more"}</span>
                <p class="text-right">{new Date(event.time*1000).getHours()}:{new Date(event.time*1000).getMinutes() < 10 ? "0" + new Date(event.time*1000).getMinutes() : new Date(event.time*1000).getMinutes()}</p>
            </div>
        </div>
    </div>
{/if}

<style>
    .status {
        transform: translate(-50%, 100%);
    }

    span {
        transform: translate(-50%);
    }
    .gunshot {
        border-bottom: 2px solid #FF4B4B;
    }

    .vehicle {
        border-bottom: 2px solid #FFB951;
    }

    .animal {
        border-bottom: 2px solid rgb(249, 249, 130);
    }

    .unknown {
        border-bottom: 2px solid grey;
    }
    button {
	background: none;
	color: inherit;
	border: none;
	padding: 0;
	font: inherit;
	cursor: pointer;
	outline: inherit;
    }   
</style>
