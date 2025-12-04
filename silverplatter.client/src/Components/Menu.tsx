import React from "react";
import "./css/Menu.css";

type MenuItem = { id: number; name: string; description?: string; price: string; tag?: string };
type MenuCategory = { id: string; title: string; items: MenuItem[] };

const sampleMenu: MenuCategory[] = [
  {
    id: "starters",
    title: "Starters",
    items: [
      { id: 1, name: "Charred Octopus", description: "Smoky paprika • Lemon aioli", price: "$12" },
      { id: 2, name: "Heirloom Tomato", description: "Burrata • Basil • Aged balsamic", price: "$10" },
    ],
  },
  {
    id: "mains",
    title: "Mains",
    items: [
      { id: 11, name: "Pan-Seared Salmon", description: "Herb butter • Seasonal veg", price: "$24", tag: "Chef" },
      { id: 12, name: "Prime Ribeye", description: "Truffle mash • Charred broccolini", price: "$32" },
    ],
  },
  {
    id: "desserts",
    title: "Desserts",
    items: [
      { id: 21, name: "Warm Chocolate Pudding", description: "Vanilla cream", price: "$8" },
      { id: 22, name: "Lemon Tart", description: "Citrus curd • Almond crust", price: "$9" },
    ],
  },
];

const Menu: React.FC = () => {
  return (
    <div className="menu-root">

      <main className="menu-main" role="main" aria-labelledby="menu-title">
        <h1 id="menu-title" className="menu-page-title">Our Menu</h1>

        <div className="menu-grid">
          <aside className="menu-categories" aria-label="Menu categories">
            <ul>
              {sampleMenu.map((c) => (
                <li key={c.id}>
                  <a href={`#${c.id}`} className="cat-link">{c.title}</a>
                </li>
              ))}
            </ul>
          </aside>

          <section className="menu-list">
            {sampleMenu.map((category) => (
              <div key={category.id} id={category.id} className="menu-category">
                <h2 className="category-title">{category.title}</h2>
                <ul className="items">
                  {category.items.map((it) => (
                    <li key={it.id} className="menu-item">
                      <div className="item-main">
                        <div className="item-title">
                          <span className="name">{it.name}</span>
                          {it.tag && <span className="tag">{it.tag}</span>}
                        </div>
                        <div className="price">{it.price}</div>
                      </div>
                      {it.description && <p className="item-desc">{it.description}</p>}
                    </li>
                  ))}
                </ul>
              </div>
            ))}
          </section>

          <aside className="menu-side" aria-label="Featured and actions">
            <div className="featured">
              <h3>Tonight's Feature</h3>
              <p className="featured-dish">Seared scallops • Brown butter • Crispy prosciutto</p>
            </div>

            <div className="reserve-card">
              <h4>Book a table</h4>
              <p>Reserve your spot for dinner service.</p>
              <button className="btn-outline">Reserve</button>
            </div>
          </aside>
        </div>
      </main>
    </div>
  );
};

export default Menu;