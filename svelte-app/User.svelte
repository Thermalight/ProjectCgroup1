<script>
    export let event;
</script>
{#if event != null}
    <div>
        <div class="content">
            <p class="text-white">username: {event.Username}</p>
            <p class="text-white">email: {event.Email}</p>
            {#if event.IsAdmin == false || event.IsAdmin == null}
                <p class="text-white">is admin: no</p>
            {:else}
            <p class="text-white">is admin: yes</p>
            {/if}
            {#if event.Subscriber == null}
            <p class="text-white">is subscribed: no</p>
            {:else}
            <p class="text-white">is subscribed: yes</p>
            {/if}
        </div>
    </div>
{/if}




