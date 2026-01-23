<template>
  <div
    class="relative border rounded-lg p-4 shadow-md hover:shadow-lg transition-shadow bg-white"
  >
    <!-- Three dots button -->
    <div class="absolute top-3 right-3">
      <button
        @click.stop="toggleMenu"
        class="text-gray-500 hover:text-gray-700"
      >
        ⋮
      </button>

      <!-- Dropdown menu -->
      <div
        v-if="showMenu"
        class="absolute right-0 mt-2 w-28 bg-white border rounded-md shadow-lg z-10"
      >
       
      </div>
    </div>

    <!-- Card content -->
    <router-link :to="`/note/${note.id}`" class="block">
      <h3 class="text-lg font-semibold text-gray-800">
        {{ note.title }}
      </h3>
      <p class="text-sm text-gray-500 mt-2">
        {{ formatDate(note.createdAt) }}
      </p>
    </router-link>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import type { Note } from '@/types'

const props = defineProps<{
  note: Note
}>()

const emit = defineEmits<{
  (e: 'delete', id: number): void
}>()

const showMenu = ref(false)

function toggleMenu() {
  showMenu.value = !showMenu.value
}


function formatDate(dateString: string): string {
  return new Date(dateString).toLocaleDateString()
}
</script>
