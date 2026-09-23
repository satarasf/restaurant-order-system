<script lang="ts">
	import { onMount } from "svelte";

    interface MenuItem{
        id: number;
        name: string;
    }

    interface OrderItem{
        id: number;
        quantity: number;
        unitPrice: number;
        note: string|null;
        menuItemId: number;
        menuItem: MenuItem | null;
    }

    interface Table{
        id: number;
        tableNumber: number;
        seats: number;
    }

    interface Order{
        id: number;
        createdAt: string;
        status: number;
        tableId: number;
        table: Table | null;
        orderItems: OrderItem[];
    }

    const statusLabels: Record<number, string> = {
        0: 'Offen',
        1: 'In Zubereitung',
        2: 'Serviert',
        3: 'Bezahlt',
    }

    let orders = $state<Order[]>([]);
    let loading = $state(true);
    let error = $state<string | null>(null);

    async function loadOrders(){
        loading = true;
        error = null;
        try{
            const response = await fetch('http://localhost:5072/api/Orders');
            if(!response.ok){
                throw new Error('Fehler beim laden der Bestellungen.');
            }
            orders = await response.json();
        }catch(err){
            error = err instanceof Error ? err.message : 'Unbekannter Fehler';
        }finally{
            loading = false;
        }
    }

    async function updateStatus(order: Order, newStatus: number){
        try{
            const orderId = order.id;
            const response = await fetch(`http://localhost:5072/api/Orders/${orderId}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({
                    tableId: order.tableId,
                    status: newStatus,
                    createtAt: order.createdAt,
                })
            });

            if(!response.ok){
                throw new Error('Fehler beim Aktualisieren des Status');
            }

            order.status = newStatus;
        }catch(err){
            error = err instanceof Error ? err.message : 'Unbekannter Fehler';
        }
    }

    function calculateTotal(order: Order): number{
        return order.orderItems
                .reduce((sum, item) => sum + item.unitPrice * item.quantity, 0);
    }


    onMount(() => {
        loadOrders();
    })
</script>

<main>
    <h1>Bestellungen</h1>

    <button class="refresh" onclick={loadOrders}>Aktualisieren</button>

    {#if loading}
        <p>Ladet...</p>
    {:else if error}
        <p class="error">{error}</p>
    {:else if orders.length === 0}
        <p>Keine Bestellungen vorhanden.</p>
    {:else}
        <div class="orders-list">
            {#each orders as order (order.id)}
                <div class="order-card">
                    <div class="order-header">
                        <span class="table-info">
                            Tisch {order.table?.tableNumber ?? order.tableId}
                        </span>
                        <select 
                            value={order.status}
                            onchange={(e) => updateStatus(order, Number(e.currentTarget.value))}
                        >
                            {#each Object.entries(statusLabels) as [value, label] (value)}
                                <option value={value}>{label}</option>
                            {/each}
                        </select>
                    </div>

                    <div class="order-items">
                        {#each order.orderItems as item (item.id)}
                            <div class="order-item">
                                <span>{item.quantity}X {item.menuItem?.name ?? 'Unbekanntes Gericht'}</span>
                                <span>{(item.unitPrice * item.quantity).toFixed(2)}€</span>
                            </div>
                        {/each}
                    </div>

                    <div class="order-total">
                        Gesamt: {calculateTotal(order).toFixed(2)} €
                    </div>
                </div>
            {/each}
        </div>
    {/if}
</main>

<style>
	main {
		max-width: 700px;
		margin: 0 auto;
		padding: 1rem;
		font-family: sans-serif;
	}

	.refresh {
		margin-bottom: 1rem;
		padding: 0.5rem 1rem;
		border: 1px solid #ddd;
		border-radius: 6px;
		background: white;
		cursor: pointer;
	}

	.orders-list {
		display: flex;
		flex-direction: column;
		gap: 1rem;
	}

	.order-card {
		border: 1px solid #ddd;
		border-radius: 8px;
		padding: 1rem;
	}

	.order-header {
		display: flex;
		justify-content: space-between;
		align-items: center;
		margin-bottom: 0.75rem;
	}

	.table-info {
		font-weight: bold;
	}

	select {
		padding: 0.3rem 0.5rem;
		border-radius: 6px;
		border: 1px solid #ccc;
	}

	.order-items {
		display: flex;
		flex-direction: column;
		gap: 0.25rem;
		margin-bottom: 0.5rem;
	}

	.order-item {
		display: flex;
		justify-content: space-between;
		font-size: 0.9rem;
		color: #444;
	}

	.order-total {
		text-align: right;
		font-weight: bold;
		border-top: 1px solid #eee;
		padding-top: 0.5rem;
	}

	.error {
		color: red;
	}
</style>