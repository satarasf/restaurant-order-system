<script lang="ts">
	import { cart } from '$lib/cart.svelte';
	import { SvelteMap } from 'svelte/reactivity';

	interface Category {
		id: number;
		name: string,
	}

	interface MenuItem {
		id: number;
		name: string;
		description: string | null;
		price: number;
		isAvailable: boolean;
		imageUrl: string | null;

		categoryId: number;
		category: Category | null;
	}

	let menuItems = $state<MenuItem[]>([]);
	let loading = $state(true);
	let error = $state<string | null>(null);

	async function loadMenuItems(){
		try{
			const response = await fetch('http://localhost:5072/api/MenuItems');
			if(!response.ok){
				throw new Error('Fehler beim Laden der Speisekarte');
			}
			menuItems = await response.json();
		}catch(err){
			error = err instanceof Error ? err.message : 'Unbekannter Fehler';
		}finally{ // wird immer ausgeführt. Egal ob es funktioniert oder nicht.
			loading = false;
		}
	}

	loadMenuItems();
	
	// Gerichte nach Kategorien Gruppieren
	let groupedItems = $derived.by(() => {
		const groups = new SvelteMap<string, MenuItem[]>();

		for(const item of menuItems){
			const categoryName = item.category?.name ?? 'Ohne Kategorie';
			if (!groups.has(categoryName)){
				groups.set(categoryName, []);
			}
			groups.get(categoryName)!.push(item);
		}
		return groups;
	});

	// -------------------------------------------Rückgabe-Typ
	function getQuantityInCart(menuItemId: number): number {
		const item = cart.items.find((i) => i.menuItemId === menuItemId);
		return item?.quantity ?? 0;
	}
	
</script>

<main>
	<h1>Speisekarte</h1>
	{#if loading}
		<p>Lädt...</p>
	{:else if error}
		<p class="error">{error}</p>
	{:else if menuItems.length === 0}
		<p>Keine Gerichte gefunden.</p>
	{:else}
		{#each groupedItems as [categoryName, items] (categoryName)}
			<section class="category-section">
				<h2>{categoryName}</h2>
				<div class="menu-list">
					{#each items as item (item.id)}
						<div class="menu-item">
							<div class="menu-item-info">
								<h3>{item.name}</h3>
								{#if item.description}
									<p>{item.description}</p>
								{/if}
								<span class="price">
									{item.price.toFixed(2)} €
								</span>
							</div>
						</div>
						<div class="menu-item-actions">
		{#if getQuantityInCart(item.id) > 0}
			<button onclick={() => cart.remove(item.id)}>−</button>
			<span class="quantity">{getQuantityInCart(item.id)}</span>
		{/if}
		<button onclick={() => cart.add(item.id, item.name, item.price)}>+</button>
	</div>
					{/each}
				</div>
			</section>
		{/each}
	{/if}
</main>

{#if cart.totalItems > 0}
	<div class="cart-bar">
		<span>{cart.totalItems} Artikel · {cart.totalPrice.toFixed(2)} €</span>
		<a href="/checkout">Zur Bestellung</a>
	</div>
{/if}

<style>
	main {
		max-width: 600px;
		margin: 0 auto;
		padding: 1rem;
		padding-bottom: 5rem;
		font-family: sans-serif;
	}

	.category-section {
		margin-bottom: 2rem;
	}

	h2 {
		font-size: 1rem;
		text-transform: uppercase;
		color: #64748b;
		letter-spacing: 0.05em;
		margin-bottom: 0.75rem;
	}

	.menu-list {
		display: flex;
		flex-direction: column;
		gap: 1rem;
	}

	.menu-item {
		display: flex;
		justify-content: space-between;
		align-items: center;
		border: 1px solid #ddd;
		border-radius: 8px;
		padding: 1rem;
	}

	.price {
		font-weight: bold;
	}

	.menu-item-actions {
		display: flex;
		align-items: center;
		gap: 0.5rem;
	}

	.menu-item-actions button {
		width: 32px;
		height: 32px;
		border-radius: 50%;
		border: none;
		background: #2563eb;
		color: white;
		font-size: 1.2rem;
		cursor: pointer;
	}

	.quantity {
		min-width: 20px;
		text-align: center;
	}

	.error {
		color: red;
	}

	.cart-bar {
		position: fixed;
		bottom: 0;
		left: 0;
		right: 0;
		background: #16a34a;
		color: white;
		padding: 1rem;
		display: flex;
		justify-content: space-between;
		align-items: center;
		max-width: 600px;
		margin: 0 auto;
	}

	.cart-bar a {
		color: white;
		font-weight: bold;
		text-decoration: none;
	}
</style>