// Generated by Sichem at 07.03.2020 16:58:11

using System;
using System.Runtime.InteropServices;

namespace StbTrueTypeSharp
{
	unsafe partial class StbTrueType
	{
		[StructLayout(LayoutKind.Sequential)]
		public struct stbtt__bitmap
		{
			public int w;
			public int h;
			public int stride;
			public byte* pixels;
		}
		[StructLayout(LayoutKind.Sequential)]
		public struct stbtt__edge
		{
			public float x0;
			public float y0;
			public float x1;
			public float y1;
			public int invert;
		}
		[StructLayout(LayoutKind.Sequential)]
		public struct stbtt__active_edge
		{
			public stbtt__active_edge* next;
			public float fx;
			public float fdx;
			public float fdy;
			public float direction;
			public float sy;
			public float ey;
		}

		public static void stbtt__handle_clipped_edge(float* scanline, int x, stbtt__active_edge* e, float x0, float y0, float x1, float y1)
		{
			if ((y0) == (y1))
				return;
			if ((y0) > (e->ey))
				return;
			if ((y1) < (e->sy))
				return;
			if ((y0) < (e->sy))
			{
				x0 += (float)((x1 - x0) * (e->sy - y0) / (y1 - y0));
				y0 = (float)(e->sy);
			}

			if ((y1) > (e->ey))
			{
				x1 += (float)((x1 - x0) * (e->ey - y1) / (y1 - y0));
				y1 = (float)(e->ey);
			}

			if ((x0 <= x) && (x1 <= x))
			{
				scanline[x] += (float)(e->direction * (y1 - y0));
			}
			else if (((x0) >= (x + 1)) && ((x1) >= (x + 1)))
			{
			}
			else
			{
				scanline[x] += (float)(e->direction * (y1 - y0) * (1 - ((x0 - x) + (x1 - x)) / 2));
			}

		}

		public static void stbtt__fill_active_edges_new(float* scanline, float* scanline_fill, int len, stbtt__active_edge* e, float y_top)
		{
			float y_bottom = (float)(y_top + 1);
			while ((e) != null)
			{
				if ((e->fdx) == (0))
				{
					float x0 = (float)(e->fx);
					if ((x0) < (len))
					{
						if ((x0) >= (0))
						{
							stbtt__handle_clipped_edge(scanline, (int)(x0), e, (float)(x0), (float)(y_top), (float)(x0), (float)(y_bottom));
							stbtt__handle_clipped_edge(scanline_fill - 1, (int)((int)(x0) + 1), e, (float)(x0), (float)(y_top), (float)(x0), (float)(y_bottom));
						}
						else
						{
							stbtt__handle_clipped_edge(scanline_fill - 1, (int)(0), e, (float)(x0), (float)(y_top), (float)(x0), (float)(y_bottom));
						}
					}
				}
				else
				{
					float x0 = (float)(e->fx);
					float dx = (float)(e->fdx);
					float xb = (float)(x0 + dx);
					float x_top = 0;
					float x_bottom = 0;
					float sy0 = 0;
					float sy1 = 0;
					float dy = (float)(e->fdy);
					if ((e->sy) > (y_top))
					{
						x_top = (float)(x0 + dx * (e->sy - y_top));
						sy0 = (float)(e->sy);
					}
					else
					{
						x_top = (float)(x0);
						sy0 = (float)(y_top);
					}
					if ((e->ey) < (y_bottom))
					{
						x_bottom = (float)(x0 + dx * (e->ey - y_top));
						sy1 = (float)(e->ey);
					}
					else
					{
						x_bottom = (float)(xb);
						sy1 = (float)(y_bottom);
					}
					if (((((x_top) >= (0)) && ((x_bottom) >= (0))) && ((x_top) < (len))) && ((x_bottom) < (len)))
					{
						if (((int)(x_top)) == ((int)(x_bottom)))
						{
							float height = 0;
							int x = (int)(x_top);
							height = (float)(sy1 - sy0);
							scanline[x] += (float)(e->direction * (1 - ((x_top - x) + (x_bottom - x)) / 2) * height);
							scanline_fill[x] += (float)(e->direction * height);
						}
						else
						{
							int x = 0;
							int x1 = 0;
							int x2 = 0;
							float y_crossing = 0;
							float step = 0;
							float sign = 0;
							float area = 0;
							if ((x_top) > (x_bottom))
							{
								float t = 0;
								sy0 = (float)(y_bottom - (sy0 - y_top));
								sy1 = (float)(y_bottom - (sy1 - y_top));
								t = (float)(sy0);
								sy0 = (float)(sy1);
								sy1 = (float)(t);
								t = (float)(x_bottom);
								x_bottom = (float)(x_top);
								x_top = (float)(t);
								dx = (float)(-dx);
								dy = (float)(-dy);
								t = (float)(x0);
								x0 = (float)(xb);
								xb = (float)(t);
							}
							x1 = ((int)(x_top));
							x2 = ((int)(x_bottom));
							y_crossing = (float)((x1 + 1 - x0) * dy + y_top);
							sign = (float)(e->direction);
							area = (float)(sign * (y_crossing - sy0));
							scanline[x1] += (float)(area * (1 - ((x_top - x1) + (x1 + 1 - x1)) / 2));
							step = (float)(sign * dy);
							for (x = (int)(x1 + 1); (x) < (x2); ++x)
							{
								scanline[x] += (float)(area + step / 2);
								area += (float)(step);
							}
							y_crossing += (float)(dy * (x2 - (x1 + 1)));
							scanline[x2] += (float)(area + sign * (1 - ((x2 - x2) + (x_bottom - x2)) / 2) * (sy1 - y_crossing));
							scanline_fill[x2] += (float)(sign * (sy1 - sy0));
						}
					}
					else
					{
						int x = 0;
						for (x = (int)(0); (x) < (len); ++x)
						{
							float y0 = (float)(y_top);
							float x1 = (float)(x);
							float x2 = (float)(x + 1);
							float x3 = (float)(xb);
							float y3 = (float)(y_bottom);
							float y1 = (float)((x - x0) / dx + y_top);
							float y2 = (float)((x + 1 - x0) / dx + y_top);
							if (((x0) < (x1)) && ((x3) > (x2)))
							{
								stbtt__handle_clipped_edge(scanline, (int)(x), e, (float)(x0), (float)(y0), (float)(x1), (float)(y1));
								stbtt__handle_clipped_edge(scanline, (int)(x), e, (float)(x1), (float)(y1), (float)(x2), (float)(y2));
								stbtt__handle_clipped_edge(scanline, (int)(x), e, (float)(x2), (float)(y2), (float)(x3), (float)(y3));
							}
							else if (((x3) < (x1)) && ((x0) > (x2)))
							{
								stbtt__handle_clipped_edge(scanline, (int)(x), e, (float)(x0), (float)(y0), (float)(x2), (float)(y2));
								stbtt__handle_clipped_edge(scanline, (int)(x), e, (float)(x2), (float)(y2), (float)(x1), (float)(y1));
								stbtt__handle_clipped_edge(scanline, (int)(x), e, (float)(x1), (float)(y1), (float)(x3), (float)(y3));
							}
							else if (((x0) < (x1)) && ((x3) > (x1)))
							{
								stbtt__handle_clipped_edge(scanline, (int)(x), e, (float)(x0), (float)(y0), (float)(x1), (float)(y1));
								stbtt__handle_clipped_edge(scanline, (int)(x), e, (float)(x1), (float)(y1), (float)(x3), (float)(y3));
							}
							else if (((x3) < (x1)) && ((x0) > (x1)))
							{
								stbtt__handle_clipped_edge(scanline, (int)(x), e, (float)(x0), (float)(y0), (float)(x1), (float)(y1));
								stbtt__handle_clipped_edge(scanline, (int)(x), e, (float)(x1), (float)(y1), (float)(x3), (float)(y3));
							}
							else if (((x0) < (x2)) && ((x3) > (x2)))
							{
								stbtt__handle_clipped_edge(scanline, (int)(x), e, (float)(x0), (float)(y0), (float)(x2), (float)(y2));
								stbtt__handle_clipped_edge(scanline, (int)(x), e, (float)(x2), (float)(y2), (float)(x3), (float)(y3));
							}
							else if (((x3) < (x2)) && ((x0) > (x2)))
							{
								stbtt__handle_clipped_edge(scanline, (int)(x), e, (float)(x0), (float)(y0), (float)(x2), (float)(y2));
								stbtt__handle_clipped_edge(scanline, (int)(x), e, (float)(x2), (float)(y2), (float)(x3), (float)(y3));
							}
							else
							{
								stbtt__handle_clipped_edge(scanline, (int)(x), e, (float)(x0), (float)(y0), (float)(x3), (float)(y3));
							}
						}
					}
				}
				e = e->next;
			}
		}

		public static void stbtt__rasterize_sorted_edges(stbtt__bitmap* result, stbtt__edge* e, int n, int vsubsample, int off_x, int off_y, void* userdata)
		{
			stbtt__hheap hh = new stbtt__hheap();
			stbtt__active_edge* active = (null);
			int y = 0;
			int j = (int)(0);
			int i = 0;
			float* scanline_data = stackalloc float[129];
			float* scanline;
			float* scanline2;
			if ((result->w) > (64))
				scanline = (float*)(CRuntime.malloc((ulong)((result->w * 2 + 1) * sizeof(float))));
			else
				scanline = scanline_data;
			scanline2 = scanline + result->w;
			y = (int)(off_y);
			e[n].y0 = (float)((float)(off_y + result->h) + 1);
			while ((j) < (result->h))
			{
				float scan_y_top = (float)(y + 0.0f);
				float scan_y_bottom = (float)(y + 1.0f);
				stbtt__active_edge** step = &active;
				CRuntime.memset(scanline, (int)(0), (ulong)(result->w * sizeof(float)));
				CRuntime.memset(scanline2, (int)(0), (ulong)((result->w + 1) * sizeof(float)));
				while ((*step) != null)
				{
					stbtt__active_edge* z = *step;
					if (z->ey <= scan_y_top)
					{
						*step = z->next;
						z->direction = (float)(0);
						stbtt__hheap_free(&hh, z);
					}
					else
					{
						step = &((*step)->next);
					}
				} while (e->y0 <= scan_y_bottom)
				{
					if (e->y0 != e->y1)
					{
						stbtt__active_edge* z = stbtt__new_active(&hh, e, (int)(off_x), (float)(scan_y_top), userdata);
						if (z != (null))
						{
							if (((j) == (0)) && (off_y != 0))
							{
								if ((z->ey) < (scan_y_top))
								{
									z->ey = (float)(scan_y_top);
								}
							}
							z->next = active;
							active = z;
						}
					}
					++e;
				}
				if ((active) != null)
					stbtt__fill_active_edges_new(scanline, scanline2 + 1, (int)(result->w), active, (float)(scan_y_top));
				{
					float sum = (float)(0);
					for (i = (int)(0); (i) < (result->w); ++i)
					{
						float k = 0;
						int m = 0;
						sum += (float)(scanline2[i]);
						k = (float)(scanline[i] + sum);
						k = (float)((float)(CRuntime.fabs((double)(k))) * 255 + 0.5f);
						m = ((int)(k));
						if ((m) > (255))
							m = (int)(255);
						result->pixels[j * result->stride + i] = ((byte)(m));
					}
				}
				step = &active;
				while ((*step) != null)
				{
					stbtt__active_edge* z = *step;
					z->fx += (float)(z->fdx);
					step = &((*step)->next);
				}
				++y;
				++j;
			}
			stbtt__hheap_cleanup(&hh, userdata);
			if (scanline != scanline_data)
				CRuntime.free(scanline);
		}

		public static void stbtt__sort_edges_ins_sort(stbtt__edge* p, int n)
		{
			int i = 0;
			int j = 0;
			for (i = (int)(1); (i) < (n); ++i)
			{
				stbtt__edge t = (stbtt__edge)(p[i]);
				stbtt__edge* a = &t;
				j = (int)(i);
				while ((j) > (0))
				{
					stbtt__edge* b = &p[j - 1];
					int c = (int)(a->y0 < b->y0 ? 1 : 0);
					if (c == 0)
						break;
					p[j] = (stbtt__edge)(p[j - 1]);
					--j;
				}
				if (i != j)
					p[j] = (stbtt__edge)(t);
			}
		}

		public static void stbtt__sort_edges_quicksort(stbtt__edge* p, int n)
		{
			while ((n) > (12))
			{
				stbtt__edge t = new stbtt__edge();
				int c01 = 0;
				int c12 = 0;
				int c = 0;
				int m = 0;
				int i = 0;
				int j = 0;
				m = (int)(n >> 1);
				c01 = (int)(((&p[0])->y0) < ((&p[m])->y0) ? 1 : 0);
				c12 = (int)(((&p[m])->y0) < ((&p[n - 1])->y0) ? 1 : 0);
				if (c01 != c12)
				{
					int z = 0;
					c = (int)(((&p[0])->y0) < ((&p[n - 1])->y0) ? 1 : 0);
					z = (int)(((c) == (c12)) ? 0 : n - 1);
					t = (stbtt__edge)(p[z]);
					p[z] = (stbtt__edge)(p[m]);
					p[m] = (stbtt__edge)(t);
				}
				t = (stbtt__edge)(p[0]);
				p[0] = (stbtt__edge)(p[m]);
				p[m] = (stbtt__edge)(t);
				i = (int)(1);
				j = (int)(n - 1);
				for (; ; )
				{
					for (i = (int)(i); ; ++i)
					{
						if (!(((&p[i])->y0) < ((&p[0])->y0)))
							break;
					}
					for (j = (int)(j); ; --j)
					{
						if (!(((&p[0])->y0) < ((&p[j])->y0)))
							break;
					}
					if ((i) >= (j))
						break;
					t = (stbtt__edge)(p[i]);
					p[i] = (stbtt__edge)(p[j]);
					p[j] = (stbtt__edge)(t);
					++i;
					--j;
				}
				if ((j) < (n - i))
				{
					stbtt__sort_edges_quicksort(p, (int)(j));
					p = p + i;
					n = (int)(n - i);
				}
				else
				{
					stbtt__sort_edges_quicksort(p + i, (int)(n - i));
					n = (int)(j);
				}
			}
		}

		public static void stbtt__sort_edges(stbtt__edge* p, int n)
		{
			stbtt__sort_edges_quicksort(p, (int)(n));
			stbtt__sort_edges_ins_sort(p, (int)(n));
		}

		public static void stbtt__rasterize(stbtt__bitmap* result, stbtt__point* pts, int* wcount, int windings, float scale_x, float scale_y, float shift_x, float shift_y, int off_x, int off_y, int invert, void* userdata)
		{
			float y_scale_inv = (float)((invert) != 0 ? -scale_y : scale_y);
			stbtt__edge* e;
			int n = 0;
			int i = 0;
			int j = 0;
			int k = 0;
			int m = 0;
			int vsubsample = (int)(1);
			n = (int)(0);
			for (i = (int)(0); (i) < (windings); ++i)
			{
				n += (int)(wcount[i]);
			}
			e = (stbtt__edge*)(CRuntime.malloc((ulong)(sizeof(stbtt__edge) * (n + 1))));
			if ((e) == (null))
				return;
			n = (int)(0);
			m = (int)(0);
			for (i = (int)(0); (i) < (windings); ++i)
			{
				stbtt__point* p = pts + m;
				m += (int)(wcount[i]);
				j = (int)(wcount[i] - 1);
				for (k = (int)(0); (k) < (wcount[i]); j = (int)(k++))
				{
					int a = (int)(k);
					int b = (int)(j);
					if ((p[j].y) == (p[k].y))
						continue;
					e[n].invert = (int)(0);
					if ((((invert) != 0) && ((p[j].y) > (p[k].y))) || ((invert == 0) && ((p[j].y) < (p[k].y))))
					{
						e[n].invert = (int)(1);
						a = (int)(j);
						b = (int)(k);
					}
					e[n].x0 = (float)(p[a].x * scale_x + shift_x);
					e[n].y0 = (float)((p[a].y * y_scale_inv + shift_y) * vsubsample);
					e[n].x1 = (float)(p[b].x * scale_x + shift_x);
					e[n].y1 = (float)((p[b].y * y_scale_inv + shift_y) * vsubsample);
					++n;
				}
			}
			stbtt__sort_edges(e, (int)(n));
			stbtt__rasterize_sorted_edges(result, e, (int)(n), (int)(vsubsample), (int)(off_x), (int)(off_y), userdata);
			CRuntime.free(e);
		}

		public static void stbtt_Rasterize(stbtt__bitmap* result, float flatness_in_pixels, stbtt_vertex* vertices, int num_verts, float scale_x, float scale_y, float shift_x, float shift_y, int x_off, int y_off, int invert, void* userdata)
		{
			float scale = (float)((scale_x) > (scale_y) ? scale_y : scale_x);
			int winding_count = (int)(0);
			int* winding_lengths = (null);
			stbtt__point* windings = stbtt_FlattenCurves(vertices, (int)(num_verts), (float)(flatness_in_pixels / scale), &winding_lengths, &winding_count, userdata);
			if ((windings) != null)
			{
				stbtt__rasterize(result, windings, winding_lengths, (int)(winding_count), (float)(scale_x), (float)(scale_y), (float)(shift_x), (float)(shift_y), (int)(x_off), (int)(y_off), (int)(invert), userdata);
				CRuntime.free(winding_lengths);
				CRuntime.free(windings);
			}

		}
	}
}