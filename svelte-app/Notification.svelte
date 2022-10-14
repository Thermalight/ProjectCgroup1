<script>
    import { slide } from 'svelte/transition';
    import Status from './Status.svelte'

    export let event;
    let open = false
    let time = new Date(event.Time*1000)
    let hours = time.getHours()
    let minutes = time.getMinutes() < 10 ? "0" + time.getMinutes() : time.getMinutes()
    
    function round(number) {
        return Math.round(number*100000)/100000
    }

    function toggle() {
        open = !open
    }
</script>

{#if event != null}
    <div on:click={toggle} class="{event.sound_type} p-4 text-white bg-primary-dark mb-2 rounded-lg">
        <div class="content">
            <p>{event.sound_type}</p>
            <Status status={event.StatusID} />
            <p>latitude: {round(event.Latitude)}</p>
            <p>longitude: {round(event.Longitude)}</p>
            {#if !open}
                <span class="absolute left-1/2">open</span>
            {:else}
                <div transition:slide>
                    <p>Probability {event.Probability}</p>
                    <p>Node {event.NodeID}</p>
                </div>
                <span class="absolute left-1/2">close</span>
            {/if}
            <p class="text-right">{hours}:{minutes}</p>
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
</style>
