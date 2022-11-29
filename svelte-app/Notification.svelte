<script>
    import { slide } from 'svelte/transition';
    import Status from './Status.svelte'
    export let mapComponent
    export let event;
    let open = false
    let play = true
    let pause = false
    let time = new Date(event.Time*1000)
    let hours = time.getHours()
    let minutes = time.getMinutes() < 10 ? "0" + time.getMinutes() : time.getMinutes()
    
    function round(number) {
        return Math.round(number*100000)/100000
    }

    function toggle() {
        open = !open
    }
    function toggleSound() {
        play = !play
        pause = !pause
    }
</script>

{#if event != null}
    <div on:click={mapComponent.flyToLocation(event.Latitude, event.Longitude)}>
        <div on:click={toggle} class="{event.sound_type} p-4 text-white bg-primary-dark mb-2 rounded-lg">
            <div class="content">
                <audio id="player" src="{event.Sound}"></audio>
                {#if play == true}
                    <button on:click={toggleSound} class="border-transparent focus:border-transparent focus:ring-0" onclick="document.getElementById('player').play()"><span class="material-symbols-outlined">play_circle</span></button>
                {:else}
                    <button on:click={toggleSound} class="border-transparent focus:border-transparent focus:ring-0 " onclick="document.getElementById('player').pause()"><span class="material-symbols-outlined">pause_circle</span></button>
                {/if}
                

                <p class="font-bold">{event.sound_type}</p>
                <Status status={event.StatusID} GUID={event.Guid}/>
                <p class="text-gray-400">latitude: {round(event.Latitude)}</p>
                <p class="text-gray-400">longitude: {round(event.Longitude)}</p>
                {#if open}
                    <div transition:slide>
                        <p class="text-gray-400">Probability: {event.Probability}</p>
                        <p class="text-gray-400">Node: {event.NodeID}</p>
                    </div>
                {/if}
                <span transition:slide class="absolute left-1/2 pt-3 material-symbols-outlined">{ open? "expand_less" : "expand_more"}</span>
                <p class="text-right">{hours}:{minutes}</p>
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
