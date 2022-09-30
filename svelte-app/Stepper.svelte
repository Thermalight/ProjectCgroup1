<script>
    export let count = 0;
  </script>
  
  <button aria-label="decrement" on:click={() => count--}>-</button>
  <span data-cy="count">{count}</span>
  <button aria-label="increment" on:click={() => count++}>+</button>