<script lang="ts">
    import { cart } from '$lib/cart.svelte';
    // import { goto } from '$app/navigation';
    import { resolve } from '$app/paths';

    let tableId = $state(1); // vorerst fix; sp#teer zb aus der URL.
    let submitting  = $state(false);
    let error = $state<string | null>(null);
    let success = $state(false);

    async function submitOrder() {
        submitting = true;
        error = null;

        try{
            const orderResponse = await fetch('http://localhost:5072/api/orders', {
                method: 'POST',
                headers:  { 'Content-Type': 'application/json' },
                body: JSON.stringify({
                    tableId: tableId,
                    status: 0 // 0 = open; ->OrderStatus Enum
                })
            });

            if(!orderResponse.ok){
                throw new Error('Fehler beim Erstellen der Bestellung');
            }

            const order = await orderResponse.json();

            for(const item of cart.items){
                const itemResponse = await fetch('http://localhost:5072/api/OrderItems', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },

                    body: JSON.stringify({
                        orderId: order.id,
                        menuItemId: item.menuItemId,
                        quantity: item.quantity,
                        unitPrice: item.price
                    })
                });

                if(!itemResponse.ok){
                    throw new Error('Fehler beim Speichern der Bestellposition');
                }
            }

            success = true;
            cart.clear();
        }catch(err){
            error = err instanceof Error ? err.message : 'unbekannter Fehler';
        }finally{
            submitting = false;
        }
    }
</script>

<main>
    <h1>Bestellung</h1>

    {#if success}
        <div class="success">
            <p>Deine Bestellung wurde erfolgreich aufgegeben!</p>
            <a href={resolve('/')}>Zurück zur Speisekarte</a>
        </div>
    {:else if cart.items.length === 0}
        <p>Dein Warenkorp ist leer.</p>
        <a href={resolve('/')}>Zurück zur Speisekarte</a>
    {:else}
        <div class="cart-items">
            {#each cart.items as item (item.menuItemId)}
                <div class="cart-item">
                    <span>{item.quantity}x {item.name}</span>
                    <span>{(item.price * item.quantity).toFixed(2)} €</span>
                </div>
            {/each}
        </div>

        <div class="total">
            <strong>Gesamt: { cart.totalPrice.toFixed(2)} €</strong>
        </div>

        {#if error}
            <p class="error">{error}</p>
        {/if}

        <button onclick={submitOrder} disabled={submitting}>
            { submitting ? 'Wird gesendet...' : 'jetzt bestellen'}
        </button>
    {/if}
</main>

<style>
	main {
		max-width: 600px;
		margin: 0 auto;
		padding: 1rem;
		font-family: sans-serif;
	}

	.cart-items {
		display: flex;
		flex-direction: column;
		gap: 0.5rem;
		margin-bottom: 1rem;
	}

	.cart-item {
		display: flex;
		justify-content: space-between;
		padding: 0.5rem 0;
		border-bottom: 1px solid #eee;
	}

	.total {
		text-align: right;
		font-size: 1.2rem;
		margin-bottom: 1.5rem;
	}

	button {
		width: 100%;
		padding: 0.75rem;
		background: #16a34a;
		color: white;
		border: none;
		border-radius: 8px;
		font-size: 1rem;
		cursor: pointer;
	}

	button:disabled {
		background: #94a3b8;
		cursor: not-allowed;
	}

	.error {
		color: red;
	}

	.success {
		text-align: center;
	}

	.success a {
		display: inline-block;
		margin-top: 1rem;
		color: #2563eb;
	}
</style>