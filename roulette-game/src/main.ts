/**
 * main.ts
 *
 * Bootstraps Vuetify and other plugins then mounts the App`
 */

// Plugins
import { registerPlugins } from '@/plugins'

// Components
import App from './App.vue'

// Composables
import { createApp } from 'vue'

//Routes
import router from './routes/router.ts';

const app = createApp(App)

registerPlugins(app)

app.use(router)
app.mount('#app')
