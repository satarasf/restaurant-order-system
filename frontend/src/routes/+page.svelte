<script lang="ts">
	interface Category {
		id: number;
		name: string;
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

	async function loadMenuItems() {
		try {
			const response = await fetch('http://localhost:5072/api/MenuItems');
			if (!response.ok) {
				throw new Error('Fehler beim Laden der Speisekarte');
			}
			menuItems = await response.json();
		} catch (err) {
			error = err instanceof Error ? err.message : 'Unbekannter Fehler';
		} finally {
			loading = false;
		}
	}

	loadMenuItems();
</script>

<main>
	<h1>Speisekarte</h1>

	{#if loading}
		<p>Lädt...</p>
	{:else if error}
		<p class="error">{error}</p>
	{:else if menuItems.length === 0}
		<p>Keine Gerichte gefunden. Leg zuerst welche über Swagger an.</p>
	{:else}
		<div class="menu-list">
			{#each menuItems as item (item.id)}
				<div class="menu-item">
					<h3>{item.name}</h3>
					{#if item.description}
						<p>{item.description}</p>
					{/if}
					<span class="price">{item.price.toFixed(2)} €</span>
				</div>
			{/each}
		</div>
	{/if}
</main>

<style>
	main {
		max-width: 600px;
		margin: 0 auto;
		padding: 1rem;
		font-family: sans-serif;
	}

	.menu-list {
		display: flex;
		flex-direction: column;
		gap: 1rem;
	}

	.menu-item {
		border: 1px solid #ddd;
		border-radius: 8px;
		padding: 1rem;
	}

	.price {
		font-weight: bold;
	}

	.error {
		color: red;
	}
</style>