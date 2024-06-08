import { createRouter, createWebHistory } from "vue-router";
import HomePage from "../components/HomePage.vue";
import GamePage from "../components/GamePage.vue";

const routes = [
  {
    path: "/",
    name: "HomePage",
    component: HomePage,
  },
  {
    path: "/home",
    name: "HomePage",
    component: HomePage,
  },
  {
    path: "/play/:playerName",
    name: "GamePage",
    component: GamePage,
  },

  {
    path: "/:catchAll(.*)*", // Catch-all route
    redirect: "/home",
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
