<template>
  <div class="border rounded-lg p-4 shadow-md hover:shadow-lg transition-shadow">
    <router-link :to="`/note/${note.id}`" class="block">
      <h3 class="text-lg font-semibold text-gray-800">{{ note.title }}</h3>
      <p class="text-sm text-gray-500 mt-2">{{ formatDate(note.createdAt) }}</p>
    </router-link>
  </div>
</template>

<script setup lang="ts">
import type { Note } from '@/types'

defineProps<{
  note: Note
}>()

function formatDate(dateString: string): string {
  return new Date(dateString).toLocaleDateString()
}
</script>