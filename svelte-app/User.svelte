<script>
    export let event;
</script>
{#if event != null}
    <div>
        <div class="content">
            <p>username: {event.Username}</p>
            <p>email: {event.Email}</p>
            {#if event.IsAdmin == false || event.IsAdmin == null}
                <p>is admin: no</p>
            {:else}
                <p>is admin: yes</p>
            {/if}
            {#if event.Subscriber == null}
                <p>is subscribed: no</p>
            {:else}
                <p>is subscribed: yes</p>
            {/if}
        </div>
    </div>
{/if}

