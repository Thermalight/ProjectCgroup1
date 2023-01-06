<script>
    export let user;
</script>
{#if user}
    <div>
        <div class="content text-white">
            <p>username: {user.username}</p>
            <p>email: {user.email}</p>
            <p>admin: {user.isAdmin ? "True" : "False"}</p>
            <p>subscribed: {user.subscriber ? "True" : "False"}</p>
        </div>
    </div>
{/if}