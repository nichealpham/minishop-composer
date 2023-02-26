<template>
  <v-list align="center" v-if="template">
    <v-list-item
      v-show="template.title"
      class="content_card_title"
      align="left"
    >
      <v-list-item-title class="pl-3 pr-3 text-capitalize">
        <b>{{ title }}</b>
      </v-list-item-title>
      <slot name="title-controller" />
    </v-list-item>
    <v-progress-circular
      v-show="loading && viewMode == ViewMode.List"
      indeterminate
      :color="loadingColor || '#fefefe'"
    ></v-progress-circular>

    <v-data-table
      v-if="viewMode == ViewMode.Table && template.headers"
      :loading="loading"
      :headers="template.headers"
      :items="items"
      :items-per-page="pagination.limit"
      class="elevation-1 table_items"
      hide-default-footer
      @click:row="handleRowClick"
    >
      <!-- eslint-disable-next-line -->
      <template v-slot:item.imageTitle="{ item }">
        <div class="d-flex">
          <v-avatar color="primary" size="36"
            ><img alt="Avatar" :src="item.image" />
          </v-avatar>
          <span v-if="item.title" class="ml-3 mt-2 mb-2" style="width: 120px">{{
            item.title.trim()
          }}</span>
        </div>
      </template>
      <!-- eslint-disable-next-line -->
      <template v-slot:item.title="{ item }">
        <span v-if="item.title" class="mt-2 mb-2" style="width: 120px">{{
          item.title.trim()
        }}</span>
      </template>
      <!-- eslint-disable-next-line -->
      <template v-slot:item.profileUrl="{ item }">
        <span
          class="mt-2 mb-2"
          style="
            max-width: 110px;
            text-overflow: ellipsis;
            overflow: hidden;
            white-space: nowrap;
            display: block;
          "
        >
          {{ item.profileUrl || "NA" }}</span
        >
      </template>
      <!-- eslint-disable-next-line -->
      <template v-slot:item.gender="{ item }">
        <gender-type :gender="item.genderType" />
      </template>
      <!-- eslint-disable-next-line -->
      <template v-slot:item.dob="{ item }">
        {{ formatDob(item.dob) }}
      </template>
      <!-- eslint-disable-next-line -->
      <template v-slot:item.phone="{ item }">
        <div class="d-flex">
          <!-- <span v-if="item.phone" style="width: 100px; margin-top: 5px">{{
            item.phone.trim()
          }}</span> -->
          <v-btn
            @click="
              $event.stopPropagation();
              callPhone(item.phone);
            "
            x-small
            color="#eeeeee"
            class="service_button"
            fab
            elevation="0"
          >
            <v-icon>mdi-phone </v-icon>
          </v-btn>
        </div>
      </template>
    </v-data-table>
    <div v-else>
      <content-item
        v-show="!loading"
        v-for="(item, index) in items"
        :key="index"
        :item="item"
        @view="$emit('view', item)"
      >
        <template v-slot:controller>
          <slot name="card-controller" :item="item" />
        </template>
      </content-item>
    </div>

    <h5 v-if="noData" class="no_data">
      {{ $t("common.nodata") }}
    </h5>
    <div v-if="pageCount > 1">
      <v-pagination
        v-model="pagination.page"
        class="my-4"
        :length="pageCount"
        color="#6166f5"
        @input="render(template)"
      ></v-pagination>
    </div>
  </v-list>
</template>

<script>
import ContentItem from "./ContentItem.vue";
import { ViewMode } from "@/plugins/contants";

import GenderType from "@/components/GenderType.vue";

export default {
  components: { ContentItem, GenderType },
  props: {
    viewMode: {
      type: Number,
      default: ViewMode.Table,
    },
    loadingColor: {
      type: String,
      default: "",
    },
    template: {
      type: Object,
      default() {
        return {
          title: "",
          api: {
            url: "",
            query: {},
            headers: {},
          },
          transform: {
            id: "",
            title: "",
            content: "",
            message: null,
            image: false,
          },
        };
      },
    },
  },
  watch: {
    template: {
      deep: true,
      handler(value) {
        this.clean();
        this.render(value);
      },
    },
  },
  data() {
    return {
      ViewMode,
      selectedId: -1,
      title: "",
      pageCount: 1,
      pagination: {
        page: 1,
        limit: 10,
      },
      loading: false,
      noData: false,
      items: [],
      defaultImage: "/icons/service.png",
    };
  },
  mounted() {
    this.clean();
  },
  methods: {
    getTitleByLanguage(title) {
      if (!title) return "";
      var result = this.$t(`cardTitle.${title.toLowerCase()}`);
      if (!result.includes("cardTitle.")) return result;
      return title;
    },
    handleRowClick(value) {
      this.selectedId = value.id;
      this.$emit("view", value);
    },
    async render(template) {
      if (!template.api || !template.api.url) {
        this.noData = true;
        this.items = [];
        return;
      }
      this.title = this.getTitleByLanguage(template.title);
      this.noData = false;
      this.loading = true;
      var err;
      try {
        var { url, query, headers } = this.template.api;
        query = query || {};
        headers = headers || {};
        headers = { ...headers, ...this.pagination };
        var result = await this.$httpClient.get(url, query, headers);
        if (result.error) {
          err = result.error;
        } else {
          var { items, totals } = result;
          if (!items.length) {
            this.noData = true;
          } else {
            this.items = this.transformItems(items);
            this.pageCount = Math.ceil(totals / this.pagination.limit);
          }
        }
      } catch (error) {
        err = error;
      }
      if (err) {
        this.showError(err || err.message);
      }
      this.loading = false;
    },
    transformItems(items = []) {
      return items.map((item) => {
        var { id, title, content, message, image } = this.template.transform;
        return {
          ...item,
          id: item[id],
          title:
            typeof title == "string"
              ? item[title]
              : title.transform(item[title.field]),
          content:
            typeof content == "string"
              ? item[content]
              : content.transform(item[content.field]),
          message:
            message == null || typeof message == "string"
              ? item[message]
              : message.transform(item[message.field]), // not use message
          image:
            image == false // not use image
              ? false
              : typeof image == "object"
              ? image.transform(item[image.field])
              : image.includes("/") // image is url
              ? image
              : item[image] // image is provided
              ? item[image]
              : this.defaultImage, // image default
          item,
        };
      });
    },
    clean() {
      this.pageCount = 1;
      this.selectedId = -1;
      this.items = [];
      this.pagination = {
        page: 1,
        limit: 10,
      };
      if (!this.template) return;
      var { api } = this.template;
      if (!api) return;
      var { headers } = api;
      if (!headers) return;
      var { page, limit } = headers;
      if (page) {
        this.pagination.page = page;
      }
      if (limit) {
        this.pagination.limit = limit;
      }
    },
  },
};
</script>
